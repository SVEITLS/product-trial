using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Product_trial.Options;
using Product_trial.BLL.Exceptions;

namespace Product_trial.Extensions
{
    public static class AuthenticationBuilderExtensions
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services)
        {
            ServiceProvider serviceProvider = services.BuildServiceProvider();
            IOptions<AuthOptions> options = serviceProvider.GetRequiredService<IOptions<AuthOptions>>();

            string signingKey = options.Value.JwtSecret;
            string issuer = options.Value.JwtIssuer;

            if (string.IsNullOrEmpty(signingKey))
            {
                throw new MissingEnvironmentVariableException("JwtSecret cannot be null or empty.");
            }

            if (string.IsNullOrEmpty(issuer))
            {
                throw new MissingEnvironmentVariableException("JwtIssuer cannot be null or empty.");
            }

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(signingKey)),
                        ValidIssuer = issuer,
                        ClockSkew = TimeSpan.Zero,
                        ValidateAudience = false
                    };
                });

            return services;
        }
    }
}
