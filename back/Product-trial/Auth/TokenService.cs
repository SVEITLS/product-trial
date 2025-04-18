using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Product_trial.DAL.Models;
using Product_trial.Options;
using System.Security.Claims;

namespace Product_trial.Auth
{
    public interface ITokenService
    {
        string GenerateToken(Account account);
    }
    public class TokenService(ILogger<TokenService> logger, IOptions<AuthOptions> options) : ITokenService 
    { 
        public string GenerateToken(Account account) { 

            logger.LogInformation("Generating JWT Token for Account : {Account}", account.Username); 

            string secretKey = options.Value.JwtSecret;
            string issuer =     options.Value.JwtIssuer;
            int expiration = options.Value.JwtExpiration;

            byte[] secretKeyBytes = Convert.FromBase64String(secretKey); SymmetricSecurityKey jwtSecurityKey = new(secretKeyBytes);
            SigningCredentials credentials = new(jwtSecurityKey, SecurityAlgorithms.HmacSha256);

            SecurityTokenDescriptor descriptor = new() { 
                Subject = new ClaimsIdentity([new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub,account.Email)]),
                Expires = DateTime.UtcNow.AddMinutes(expiration),
                SigningCredentials = credentials, Issuer = issuer
            };

            JsonWebTokenHandler handler = new();

            string token = handler.CreateToken(descriptor);
            return token; 
        } 
    }
}
