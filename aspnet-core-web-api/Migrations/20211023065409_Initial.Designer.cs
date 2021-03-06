// <auto-generated />
using System;
using Infrastructure.Persistent;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace aspnet_core_web_api.Migrations
{
    [DbContext(typeof(DataDbContext))]
    [Migration("20211023065409_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.Entity.Category", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Name = "Lipstick"
                        },
                        new
                        {
                            ID = 2,
                            Name = "Cushion"
                        },
                        new
                        {
                            ID = 3,
                            Name = "Accessories"
                        },
                        new
                        {
                            ID = 4,
                            Name = "Fashion"
                        },
                        new
                        {
                            ID = 5,
                            Name = "Phone"
                        });
                });

            modelBuilder.Entity("Domain.Entity.Product", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DiscontinuedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<short>("Rating")
                        .HasColumnType("smallint");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("SupplierID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("SupplierID");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Description = "With the Touch of Rose matte lipstick collection from OFÉLIA made in the USA, you'll experience 14 captivating colors that glide on your lips like rose petals.",
                            DiscontinuedDate = new DateTime(2020, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Touch Of Rose",
                            Price = 320000.0,
                            Rating = (short)5,
                            ReleaseDate = new DateTime(2018, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SupplierID = 1
                        },
                        new
                        {
                            ID = 2,
                            Description = "OFÉLIA Modern Matte Lipstick goes on bold and stays moisturize for hours! Created with pure matte powder pigments, shea butter and rich emollients; this unique formula delivers full matte opacity, a truly comfortable supreme hydration, lightweight feel, and up to 8 hours of high-impact color.",
                            DiscontinuedDate = new DateTime(2020, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Modern Matte Lipstick",
                            Price = 330000.0,
                            Rating = (short)4,
                            ReleaseDate = new DateTime(2018, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SupplierID = 1
                        },
                        new
                        {
                            ID = 3,
                            Description = "The collection is inspired by mixing & matching lipstick colors in makeup as well as mixing & matching outfits in fashion.",
                            DiscontinuedDate = new DateTime(2023, 10, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Beauty Glasses Lip",
                            Price = 250000.0,
                            Rating = (short)5,
                            ReleaseDate = new DateTime(2018, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SupplierID = 2
                        },
                        new
                        {
                            ID = 4,
                            DiscontinuedDate = new DateTime(2025, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "T-Shirt aespa",
                            Price = 120000.0,
                            Rating = (short)3,
                            ReleaseDate = new DateTime(2018, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SupplierID = 5
                        },
                        new
                        {
                            ID = 5,
                            DiscontinuedDate = new DateTime(2025, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Mini Flower",
                            Price = 60000.0,
                            Rating = (short)4,
                            ReleaseDate = new DateTime(2019, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SupplierID = 4
                        },
                        new
                        {
                            ID = 6,
                            DiscontinuedDate = new DateTime(2025, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Pencil S",
                            Price = 30000.0,
                            Rating = (short)2,
                            ReleaseDate = new DateTime(2019, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SupplierID = 3
                        },
                        new
                        {
                            ID = 7,
                            DiscontinuedDate = new DateTime(2025, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Happy rabbit book A5",
                            Price = 15000.0,
                            Rating = (short)2,
                            ReleaseDate = new DateTime(2019, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SupplierID = 4
                        },
                        new
                        {
                            ID = 8,
                            DiscontinuedDate = new DateTime(2025, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Perfect Couple Dual Foundation",
                            Price = 390000.0,
                            Rating = (short)4,
                            ReleaseDate = new DateTime(2019, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SupplierID = 2
                        },
                        new
                        {
                            ID = 9,
                            Description = "A dramatically more powerful camera system. A display so responsive, every interaction feels new again. The world’s fastest smartphone chip. Exceptional durability. And a huge leap in battery life.",
                            DiscontinuedDate = new DateTime(2025, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "iPhone 13 Pro Max",
                            Price = 45490000.0,
                            Rating = (short)4,
                            ReleaseDate = new DateTime(2019, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SupplierID = 3
                        });
                });

            modelBuilder.Entity("Domain.Entity.ProductDetail", b =>
                {
                    b.Property<int>("ProductID")
                        .HasColumnType("int");

                    b.Property<string>("Details")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductID");

                    b.ToTable("ProductDetails");

                    b.HasData(
                        new
                        {
                            ProductID = 1,
                            Details = "Made by Ofélia Team"
                        },
                        new
                        {
                            ProductID = 4,
                            Details = "T-Shirt"
                        });
                });

            modelBuilder.Entity("Domain.Entity.Product_Category", b =>
                {
                    b.Property<int>("CategoryID")
                        .HasColumnType("int");

                    b.Property<int>("ProductID")
                        .HasColumnType("int");

                    b.HasKey("CategoryID", "ProductID");

                    b.HasIndex("ProductID");

                    b.ToTable("Product_Categories");

                    b.HasData(
                        new
                        {
                            CategoryID = 1,
                            ProductID = 1
                        },
                        new
                        {
                            CategoryID = 4,
                            ProductID = 1
                        },
                        new
                        {
                            CategoryID = 1,
                            ProductID = 2
                        },
                        new
                        {
                            CategoryID = 1,
                            ProductID = 3
                        },
                        new
                        {
                            CategoryID = 4,
                            ProductID = 4
                        },
                        new
                        {
                            CategoryID = 3,
                            ProductID = 5
                        },
                        new
                        {
                            CategoryID = 4,
                            ProductID = 5
                        },
                        new
                        {
                            CategoryID = 3,
                            ProductID = 6
                        },
                        new
                        {
                            CategoryID = 3,
                            ProductID = 7
                        },
                        new
                        {
                            CategoryID = 2,
                            ProductID = 8
                        },
                        new
                        {
                            CategoryID = 5,
                            ProductID = 9
                        });
                });

            modelBuilder.Entity("Domain.Entity.Supplier", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Suppliers");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Address = "Ho Chi Minh City",
                            Name = "Ofélia"
                        },
                        new
                        {
                            ID = 2,
                            Address = "Ho Chi Minh City",
                            Name = "Lemonade"
                        },
                        new
                        {
                            ID = 3,
                            Address = "Ho Chi Minh City",
                            Name = "Yeppi Yeppi"
                        },
                        new
                        {
                            ID = 4,
                            Address = "Ho Chi Minh City",
                            Name = "Moji"
                        },
                        new
                        {
                            ID = 5,
                            Address = "Ho Chi Minh City",
                            Name = "Shein"
                        });
                });

            modelBuilder.Entity("Domain.Entity.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            IsAdmin = true,
                            Password = "123456789",
                            Username = "giangvht"
                        },
                        new
                        {
                            ID = 2,
                            IsAdmin = false,
                            Password = "123456789",
                            Username = "giangvht1"
                        });
                });

            modelBuilder.Entity("Domain.Entity.Product", b =>
                {
                    b.HasOne("Domain.Entity.Supplier", "Supplier")
                        .WithMany("Products")
                        .HasForeignKey("SupplierID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("Supplier");
                });

            modelBuilder.Entity("Domain.Entity.ProductDetail", b =>
                {
                    b.HasOne("Domain.Entity.Product", "Product")
                        .WithOne("ProductDetail")
                        .HasForeignKey("Domain.Entity.ProductDetail", "ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Domain.Entity.Product_Category", b =>
                {
                    b.HasOne("Domain.Entity.Category", "Category")
                        .WithMany("Product_Categories")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entity.Product", "Product")
                        .WithMany("Product_Categories")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Domain.Entity.Category", b =>
                {
                    b.Navigation("Product_Categories");
                });

            modelBuilder.Entity("Domain.Entity.Product", b =>
                {
                    b.Navigation("Product_Categories");

                    b.Navigation("ProductDetail");
                });

            modelBuilder.Entity("Domain.Entity.Supplier", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
