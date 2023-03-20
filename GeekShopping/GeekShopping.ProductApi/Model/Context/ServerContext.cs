using Microsoft.EntityFrameworkCore;
using System;

namespace GeekShopping.ProductApi.Model.Context
{
    public class ServerContext : DbContext
    {
        public ServerContext(DbContextOptions<ServerContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AI");

            modelBuilder.Entity<Products>().HasData(new Products
            {
                Id = 4,
                Name = "CAMISETA DE CAMINHADA",
                Price = (decimal)69.97,
                Description = "Camiste de caminhada",
                CategoryName = "Camisetas",
                ImageUrl = "urldaqui"
            });
            modelBuilder.Entity<Products>().HasData(new Products
            {
                Id = 5,
                Name = "BARRACA DE CAMPING",
                Price = (decimal)369.97,
                Description = "Barraca simples de camping",
                CategoryName = "Camping",
                ImageUrl = "urldaqui"
            });
            modelBuilder.Entity<Products>().HasData(new Products
            {
                Id = 6,
                Name = "CAMISETA DE CAMINHADA ERGOMETRICA",
                Price = (decimal)169.97,
                Description = "Camiste de caminhada ergometrica",
                CategoryName = "Camisetas",
                ImageUrl = "urldaqui"
            });
            modelBuilder.Entity<Products>().HasData(new Products
            {
                Id = 7,
                Name = "BOLSA TERMICA DE CAMPING",
                Price = (decimal)69.97,
                Description = "Bolsa termica para camping",
                CategoryName = "Camping",
                ImageUrl = "urldaqui"
            });
        }
        public DbSet<Products> Products { get; set; }
    }
}
