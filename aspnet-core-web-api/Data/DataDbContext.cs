using Microsoft.EntityFrameworkCore;
using aspnet_core_web_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspnet_core_web_api.Data
{
    public class DataDbContext : DbContext
    {
        public DataDbContext()
        {
        }

        public DataDbContext(DbContextOptions<DataDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductDetail> ProductDetails { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Product_Category> Product_Categories { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.HasOne<ProductDetail>(product => product.ProductDetail)
                      .WithOne(pdetail => pdetail.Product)
                      .HasForeignKey<ProductDetail>(pdetail => pdetail.ProductID)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne<Supplier>(product => product.Supplier)
                      .WithMany(sp => sp.Products)
                      .HasForeignKey(product => product.SupplierID);
            });

            modelBuilder.Entity<ProductDetail>(entity =>
            {
                entity.HasKey(e => e.ProductID);
                entity.HasOne<Product>(e => e.Product)
                      .WithOne(a => a.ProductDetail);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.Name);
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.HasMany<Product>(sp => sp.Products)
                      .WithOne(product => product.Supplier)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Product_Category>(entity =>
            {
                entity.HasKey(e => new { e.CategoryID, e.ProductID });

                entity.HasOne<Product>(pc => pc.Product)
                      .WithMany(product => product.Product_Categories)
                      .HasForeignKey(pc => pc.ProductID);

                entity.HasOne<Category>(pc => pc.Category)
                      .WithMany(ct => ct.Product_Categories)
                      .HasForeignKey(pc => pc.CategoryID);
                      
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.ID);
            });
        }
    }
}
