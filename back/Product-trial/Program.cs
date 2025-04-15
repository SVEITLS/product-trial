using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;
using Product_trial.BLL.Exceptions;
using Product_trial.BLL.Services;
using Product_trial.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration.AddEnvironmentVariables();
builder.Logging.AddConsole();


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
});

builder.Services.AddScoped<IProductService, ProductService>();

var connectionString = builder.Configuration.GetValue<string>("SQL_PATH");

if(string.IsNullOrEmpty(connectionString))
{
    throw new ConnectionStringMissingException("SQL_PATH");
}

builder.Services.AddSQLLite(connectionString);  


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options =>
    {
        options.OpenApiVersion = OpenApiSpecVersion.OpenApi2_0;
    });
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
