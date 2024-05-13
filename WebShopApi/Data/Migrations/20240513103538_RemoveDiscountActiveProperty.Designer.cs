﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebShop.Data;

#nullable disable

namespace WebShop.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240513103538_RemoveDiscountActiveProperty")]
    partial class RemoveDiscountActiveProperty
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.4");

            modelBuilder.Entity("WebShop.Models.DbModels.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<int>("FkCustomerId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("FkCustomerId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("WebShop.Models.DbModels.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("WebShop.Models.DbModels.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("WebShop.Models.DbModels.CustomerOrder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("FkCustomerId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("FkOrderId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("FkCustomerId");

                    b.HasIndex("FkOrderId");

                    b.ToTable("CustomerOrders");
                });

            modelBuilder.Entity("WebShop.Models.DbModels.Discount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("Percent")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Discounts");
                });

            modelBuilder.Entity("WebShop.Models.DbModels.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsPaid")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("TotalSum")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("WebShop.Models.DbModels.OrderProducts", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("FkOrderId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("FkProductId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("FkOrderId");

                    b.HasIndex("FkProductId");

                    b.ToTable("OrderProducts");
                });

            modelBuilder.Entity("WebShop.Models.DbModels.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("AddDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<int?>("FkDiscountId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDiscontinued")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("FkDiscountId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("WebShop.Models.DbModels.ProductCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("FkCategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("FkProductId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("FkCategoryId");

                    b.HasIndex("FkProductId");

                    b.ToTable("ProductCategories");
                });

            modelBuilder.Entity("WebShop.Models.DbModels.Shipment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("DeliveryDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("FkOrderId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ShippedDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("FkOrderId");

                    b.ToTable("Shipments");
                });

            modelBuilder.Entity("WebShop.Models.DbModels.Address", b =>
                {
                    b.HasOne("WebShop.Models.DbModels.Customer", "Customer")
                        .WithMany()
                        .HasForeignKey("FkCustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("WebShop.Models.DbModels.CustomerOrder", b =>
                {
                    b.HasOne("WebShop.Models.DbModels.Customer", "Customer")
                        .WithMany("CustomerOrders")
                        .HasForeignKey("FkCustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebShop.Models.DbModels.Order", "Order")
                        .WithMany()
                        .HasForeignKey("FkOrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("WebShop.Models.DbModels.OrderProducts", b =>
                {
                    b.HasOne("WebShop.Models.DbModels.Order", "Order")
                        .WithMany("OrderProducts")
                        .HasForeignKey("FkOrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebShop.Models.DbModels.Product", "Product")
                        .WithMany("OrderProducts")
                        .HasForeignKey("FkProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("WebShop.Models.DbModels.Product", b =>
                {
                    b.HasOne("WebShop.Models.DbModels.Discount", "Discount")
                        .WithMany()
                        .HasForeignKey("FkDiscountId");

                    b.Navigation("Discount");
                });

            modelBuilder.Entity("WebShop.Models.DbModels.ProductCategory", b =>
                {
                    b.HasOne("WebShop.Models.DbModels.Category", "Category")
                        .WithMany("ProductCategories")
                        .HasForeignKey("FkCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebShop.Models.DbModels.Product", "Product")
                        .WithMany("ProductCategories")
                        .HasForeignKey("FkProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("WebShop.Models.DbModels.Shipment", b =>
                {
                    b.HasOne("WebShop.Models.DbModels.Order", "Order")
                        .WithMany()
                        .HasForeignKey("FkOrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("WebShop.Models.DbModels.Category", b =>
                {
                    b.Navigation("ProductCategories");
                });

            modelBuilder.Entity("WebShop.Models.DbModels.Customer", b =>
                {
                    b.Navigation("CustomerOrders");
                });

            modelBuilder.Entity("WebShop.Models.DbModels.Order", b =>
                {
                    b.Navigation("OrderProducts");
                });

            modelBuilder.Entity("WebShop.Models.DbModels.Product", b =>
                {
                    b.Navigation("OrderProducts");

                    b.Navigation("ProductCategories");
                });
#pragma warning restore 612, 618
        }
    }
}
