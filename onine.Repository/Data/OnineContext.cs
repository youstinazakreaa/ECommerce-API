using Microsoft.EntityFrameworkCore;
using onine.core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace onine.Repository.Data
{
    public class Oninecontext : DbContext
    {

        public Oninecontext(DbContextOptions<Oninecontext> options) : base(options)
        { }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)

        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());


            
        }
    }
}
