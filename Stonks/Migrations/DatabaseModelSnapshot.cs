﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Stonks.Plugins.Database;

namespace Stonks.Migrations
{
    [DbContext(typeof(Database))]
    partial class DatabaseModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Stonks.Models.Persons.User", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Stonks.Models.Portfolio", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("cash")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("userId")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("userId")
                        .IsUnique();

                    b.ToTable("Portfolio");
                });

            modelBuilder.Entity("Stonks.Models.Share", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("amount")
                        .HasColumnType("int");

                    b.Property<decimal>("currentValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("portfolioId")
                        .HasColumnType("int");

                    b.Property<decimal>("purchaseValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("stockId")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("portfolioId");

                    b.ToTable("Share");
                });

            modelBuilder.Entity("Stonks.Models.Stock", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("company")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("currentValue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("stockCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Stock");
                });

            modelBuilder.Entity("Stonks.Models.StockValueInTime", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("stockId")
                        .HasColumnType("int");

                    b.Property<int>("timestamp")
                        .HasColumnType("int");

                    b.Property<decimal>("value")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("id");

                    b.HasIndex("stockId");

                    b.ToTable("StockValueInTime");
                });

            modelBuilder.Entity("Stonks.Models.Transaction", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("assets")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("cash")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("portfolioId")
                        .HasColumnType("int");

                    b.Property<decimal>("value")
                        .HasColumnType("decimal(18,2)");

                    b.Property<bool>("verified")
                        .HasColumnType("bit");

                    b.HasKey("id");

                    b.HasIndex("portfolioId");

                    b.ToTable("Transaction");
                });

            modelBuilder.Entity("Stonks.Models.Portfolio", b =>
                {
                    b.HasOne("Stonks.Models.Persons.User", null)
                        .WithOne("userPortfolio")
                        .HasForeignKey("Stonks.Models.Portfolio", "userId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Stonks.Models.Share", b =>
                {
                    b.HasOne("Stonks.Models.Portfolio", null)
                        .WithMany("listOfShares")
                        .HasForeignKey("portfolioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Stonks.Models.StockValueInTime", b =>
                {
                    b.HasOne("Stonks.Models.Stock", null)
                        .WithMany("history")
                        .HasForeignKey("stockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Stonks.Models.Transaction", b =>
                {
                    b.HasOne("Stonks.Models.Portfolio", null)
                        .WithMany("transactions")
                        .HasForeignKey("portfolioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
