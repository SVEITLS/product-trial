using Microsoft.EntityFrameworkCore;
using Product_trial.DAL.Context;

namespace Product_trial.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddSQLLite(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ProductContext>(options =>
                options.UseSqlite(connectionString));

            return services;
        }
    }
}
