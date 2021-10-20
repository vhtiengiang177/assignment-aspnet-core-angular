using Microsoft.EntityFrameworkCore;
using Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Persistent
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
                      .HasForeignKey(pc => pc.ProductID)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne<Category>(pc => pc.Category)
                      .WithMany(ct => ct.Product_Categories)
                      .HasForeignKey(pc => pc.CategoryID)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.ID);
            });

            Seed(modelBuilder);
        }

        public void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Supplier>().HasData(
                new Supplier()
                {
                    ID = 1,
                    Name = "Ofélia",
                    Address = "Ho Chi Minh City"
                },
                new Supplier()
                {
                    ID = 2,
                    Name = "Lemonade",
                    Address = "Ho Chi Minh City"
                },
                new Supplier()
                {
                    ID = 3,
                    Name = "Yeppi Yeppi",
                    Address = "Ho Chi Minh City"
                },
                new Supplier()
                {
                    ID = 4,
                    Name = "Moji",
                    Address = "Ho Chi Minh City"
                },
                new Supplier()
                {
                    ID = 5,
                    Name = "Shein",
                    Address = "Ho Chi Minh City"
                });

            modelBuilder.Entity<Category>().HasData(
                new Category() { ID = 1, Name = "Lipstick" },
                new Category() { ID = 2, Name = "Cushion" },
                new Category() { ID = 3, Name = "Accessories" },
                new Category() { ID = 4, Name = "Fashion" },
                new Category() { ID = 5, Name = "Phone" });

            modelBuilder.Entity<User>().HasData(
                new User()
                {
                    ID = 1,
                    Username = "giangvht",
                    Password = "123456789",
                    IsAdmin = true
                },
                new User()
                {
                    ID = 2,
                    Username = "giangvht1",
                    Password = "123456789",
                    IsAdmin = false
                });

            modelBuilder.Entity<Product>().HasData(
                new Product() { 
                    ID = 1,
                    Name = "Touch Of Rose",
                    Description = "With the Touch of Rose matte lipstick collection from OFÉLIA " +
                    "made in the USA, you'll experience 14 captivating colors that glide on your " +
                    "lips like rose petals.",
                    ReleaseDate = new DateTime(2018, 2, 2),
                    DiscontinuedDate = new DateTime(2020, 10, 20),
                    Rating = 5,
                    Price = 320000,
                    SupplierID = 1
                },
                new Product()
                {
                    ID = 2,
                    Name = "Modern Matte Lipstick",
                    Description = "OFÉLIA Modern Matte Lipstick goes on bold and stays moisturize " +
                    "for hours! Created with pure matte powder pigments, shea butter and rich emollients; " +
                    "this unique formula delivers full matte opacity, a truly comfortable supreme hydration, " +
                    "lightweight feel, and up to 8 hours of high-impact color.",
                    ReleaseDate = new DateTime(2018, 2, 2),
                    DiscontinuedDate = new DateTime(2020, 10, 20),
                    Rating = 4,
                    Price = 330000,
                    SupplierID = 1
                },
                new Product()
                {
                    ID = 3,
                    Name = "Beauty Glasses Lip",
                    Description = "The collection is inspired by mixing & matching lipstick colors " +
                    "in makeup as well as mixing & matching outfits in fashion.",
                    ReleaseDate = new DateTime(2018, 2, 15),
                    DiscontinuedDate = new DateTime(2023, 10, 20),
                    Rating = 5,
                    Price = 250000,
                    SupplierID = 2
                },
                new Product()
                {
                    ID = 4,
                    Name = "T-Shirt aespa",
                    ReleaseDate = new DateTime(2018, 2, 15),
                    DiscontinuedDate = new DateTime(2025, 10, 25),
                    Rating = 3,
                    Price = 120000,
                    SupplierID = 5
                },
                new Product()
                {
                    ID = 5,
                    Name = "Mini Flower",
                    ReleaseDate = new DateTime(2019, 3, 22),
                    DiscontinuedDate = new DateTime(2025, 10, 25),
                    Rating = 4,
                    Price = 60000,
                    SupplierID = 4
                },
                new Product()
                {
                    ID = 6,
                    Name = "Pencil S",
                    ReleaseDate = new DateTime(2019, 3, 22),
                    DiscontinuedDate = new DateTime(2025, 10, 25),
                    Rating = 2,
                    Price = 30000,
                    SupplierID = 3
                },
                new Product()
                {
                    ID = 7,
                    Name = "Happy rabbit book A5",
                    ReleaseDate = new DateTime(2019, 3, 22),
                    DiscontinuedDate = new DateTime(2025, 10, 25),
                    Rating = 2,
                    Price = 15000,
                    SupplierID = 4
                },
                new Product()
                {
                    ID = 8,
                    Name = "Perfect Couple Dual Foundation",
                    ReleaseDate = new DateTime(2019, 3, 22),
                    DiscontinuedDate = new DateTime(2025, 10, 25),
                    Rating = 4,
                    Price = 390000,
                    SupplierID = 2
                },
                new Product()
                {
                    ID = 9,
                    Name = "iPhone 13 Pro Max",
                    Description = "A dramatically more powerful camera system. " +
                    "A display so responsive, every interaction feels new again. " +
                    "The world’s fastest smartphone chip. Exceptional durability. " +
                    "And a huge leap in battery life.",
                    ReleaseDate = new DateTime(2019, 3, 22),
                    DiscontinuedDate = new DateTime(2025, 10, 25),
                    Rating = 4,
                    Price = 45490000,
                    SupplierID = 3
                });

            modelBuilder.Entity<ProductDetail>().HasData(
                new ProductDetail() { ProductID = 1, Details = "Made by Ofélia Team" },
                new ProductDetail() { ProductID = 4, Details = "T-Shirt" });

            modelBuilder.Entity<Product_Category>().HasData(
                new Product_Category() { ProductID = 1, CategoryID = 1 },
                new Product_Category() { ProductID = 1, CategoryID = 4 },
                new Product_Category() { ProductID = 2, CategoryID = 1 },
                new Product_Category() { ProductID = 3, CategoryID = 1 },
                new Product_Category() { ProductID = 4, CategoryID = 4 },
                new Product_Category() { ProductID = 5, CategoryID = 3 },
                new Product_Category() { ProductID = 5, CategoryID = 4 },
                new Product_Category() { ProductID = 6, CategoryID = 3 },
                new Product_Category() { ProductID = 7, CategoryID = 3 },
                new Product_Category() { ProductID = 8, CategoryID = 2 },
                new Product_Category() { ProductID = 9, CategoryID = 5 });
        }
    }
}
