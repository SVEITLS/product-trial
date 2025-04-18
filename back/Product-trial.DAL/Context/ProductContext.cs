using System.Collections.Generic;
using System.Reflection.Metadata;
using Microsoft.EntityFrameworkCore;
using Product_trial.DAL.Models;

namespace Product_trial.DAL.Context
{
    public class ProductContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public ProductContext(DbContextOptions<ProductContext> options) : base(options) { }
    }
}
