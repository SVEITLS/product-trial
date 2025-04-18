using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;
using Product_trial.Auth;
using Product_trial.BLL.Exceptions;
using Product_trial.BLL.Services;
using Product_trial.Extensions;
using Product_trial.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddOptionsWithValidateOnStart<AuthOptions>()
    .Bind(builder.Configuration.GetSection(AuthOptions.Auth))
    .ValidateDataAnnotations();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Product trial API",
        Description = "An ASP.NET Core Web API for managing products items",
    });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddJwtAuthentication();

string? connectionString = builder.Configuration.GetValue<string>("SQL_PATH");

if (string.IsNullOrEmpty(connectionString))
{
    throw new ConnectionStringMissingException("SQL_PATH");
}

string? adminEmail = builder.Configuration.GetValue<string>("Auth:AdminEmail");

if (string.IsNullOrEmpty(adminEmail))
{
    throw new MissingEnvironmentVariableException("AdminEmail");
}

builder.Services.AddAuthorization(options => {
    options.AddPolicy("Admin", policy => policy.RequireClaim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", adminEmail));
});


builder.Services.AddSQLLite(connectionString);  

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options =>
    {
        options.OpenApiVersion = OpenApiSpecVersion.OpenApi3_0;
    });
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
