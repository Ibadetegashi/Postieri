﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Postieri.Models;

#nullable disable

namespace Postieri.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Postieri.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"), 1L, 1);

                    b.Property<int?>("ShelfId")
                        .HasColumnType("int");

                    b.HasKey("ProductId");

                    b.HasIndex("ShelfId");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("Postieri.Models.Shelf", b =>
                {
                    b.Property<int>("ShelfId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ShelfId"), 1L, 1);

                    b.Property<string>("BinLetter")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.Property<int>("MaxProducts")
                        .HasColumnType("int");

                    b.Property<int>("WarehouseId")
                        .HasColumnType("int");

                    b.HasKey("ShelfId");

                    b.HasIndex("WarehouseId");

                    b.ToTable("Shelves");
                });

            modelBuilder.Entity("Postieri.Models.Warehouse", b =>
                {
                    b.Property<int>("WarehouseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WarehouseId"), 1L, 1);

                    b.Property<double>("Area")
                        .HasColumnType("float");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NumOfShelves")
                        .HasColumnType("int");

                    b.HasKey("WarehouseId");

                    b.ToTable("Warehouse");
                });

            modelBuilder.Entity("Postieri.Models.Product", b =>
                {
                    b.HasOne("Postieri.Models.Shelf", null)
                        .WithMany("Products")
                        .HasForeignKey("ShelfId");
                });

            modelBuilder.Entity("Postieri.Models.Shelf", b =>
                {
                    b.HasOne("Postieri.Models.Warehouse", "Warehouse")
                        .WithMany("Shelves")
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Warehouse");
                });

            modelBuilder.Entity("Postieri.Models.Shelf", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Postieri.Models.Warehouse", b =>
                {
                    b.Navigation("Shelves");
                });
#pragma warning restore 612, 618
        }
    }
}
