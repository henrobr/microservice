using Microsoft.EntityFrameworkCore;
using System;

namespace GeekShopping.CouponApi.Model.Context
{
    public class ServerContext : DbContext
    {
        public ServerContext(DbContextOptions<ServerContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AI");

            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                Id = 1,
                CouponCode = "CP1",
                DiscountAmount = 10
            });
            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                Id = 2,
                CouponCode = "CP2",
                DiscountAmount = 15
            });
            modelBuilder.Entity<Coupon>().HasData(new Coupon
            {
                Id = 3,
                CouponCode = "CP3",
                DiscountAmount = 20
            });
        }
        public DbSet<Coupon> Coupons { get; set; }
    }
}
