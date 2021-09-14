﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspnet_core_web_api.Models
{
    public class DataDbContext : DbContext
    {
        public DataDbContext(DbContextOptions<DataDbContext> options) : base(options)
        {

        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductDetail> ProductDetails { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<Product_Category> Product_Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ID);
                entity.Property(e => e.Name);
                entity.Property(e => e.Price);
                entity.Property(e => e.Rating);
                entity.Property(e => e.ReleaseDate);
                entity.HasOne<ProductDetail>(product => product.ProductDetail)
                      .WithOne(pdetail => pdetail.Product)
                      .HasForeignKey<ProductDetail>(pdetail => pdetail.ProductID);
                entity.HasOne<Supplier>(product => product.Supplier)
                      .WithMany(sp => sp.Products)
                      .HasForeignKey(product => product.SupplierID);
            });

            modelBuilder.Entity<ProductDetail>(entity =>
            {
                entity.HasKey(e => e.ProductID);
                entity.Property(e => e.Details);
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
                      .WithOne(product => product.Supplier);
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
        }
    }
}