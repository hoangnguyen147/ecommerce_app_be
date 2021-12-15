﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using AppContext = EcommerceApp.Models.AppContext;

namespace EcommerceApp.Migrations
{
    [DbContext(typeof(AppContext))]
    partial class AppContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.5");

            modelBuilder.Entity("EcommerceApp.Models.Cart", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<DateTime>("created_at")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("message_from_shop")
                        .HasColumnType("longtext");

                    b.Property<string>("message_from_user")
                        .HasColumnType("longtext");

                    b.Property<string>("status")
                        .HasColumnType("longtext");

                    b.Property<long>("user_id")
                        .HasColumnType("bigint");

                    b.HasKey("id");

                    b.HasIndex("user_id");

                    b.ToTable("Cart");
                });

            modelBuilder.Entity("EcommerceApp.Models.CartItem", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("cart_id")
                        .HasColumnType("bigint");

                    b.Property<int>("price")
                        .HasColumnType("int");

                    b.Property<long>("product_id")
                        .HasColumnType("bigint");

                    b.Property<int>("quantity")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("cart_id");

                    b.HasIndex("product_id");

                    b.ToTable("CartItem");
                });

            modelBuilder.Entity("EcommerceApp.Models.Category", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("description")
                        .HasColumnType("longtext");

                    b.Property<string>("image")
                        .HasColumnType("longtext");

                    b.Property<string>("name")
                        .HasColumnType("longtext");

                    b.HasKey("id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("EcommerceApp.Models.Comment", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("content")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("created_at")
                        .HasColumnType("datetime(6)");

                    b.Property<long>("product_id")
                        .HasColumnType("bigint");

                    b.Property<long>("user_id")
                        .HasColumnType("bigint");

                    b.Property<float>("vote")
                        .HasColumnType("float");

                    b.HasKey("id");

                    b.HasIndex("product_id");

                    b.HasIndex("user_id");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("EcommerceApp.Models.Product", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("category_id")
                        .HasColumnType("bigint");

                    b.Property<int>("count_vote")
                        .HasColumnType("int");

                    b.Property<string>("image")
                        .HasColumnType("longtext");

                    b.Property<bool>("is_hot")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("name")
                        .HasColumnType("longtext");

                    b.Property<string>("overview")
                        .HasColumnType("longtext");

                    b.Property<int>("price")
                        .HasColumnType("int");

                    b.Property<int>("quantity")
                        .HasColumnType("int");

                    b.Property<float>("vote")
                        .HasColumnType("float");

                    b.HasKey("id");

                    b.HasIndex("category_id");

                    b.ToTable("Product");
                });

            modelBuilder.Entity("EcommerceApp.Models.User", b =>
                {
                    b.Property<long>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("address")
                        .HasColumnType("longtext");

                    b.Property<string>("avatar")
                        .HasColumnType("longtext");

                    b.Property<string>("email")
                        .HasColumnType("longtext");

                    b.Property<string>("fullname")
                        .HasColumnType("longtext");

                    b.Property<bool>("is_admin")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("password")
                        .HasColumnType("longtext");

                    b.Property<string>("phone")
                        .HasColumnType("longtext");

                    b.Property<string>("username")
                        .HasColumnType("longtext");

                    b.HasKey("id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("EcommerceApp.Models.Cart", b =>
                {
                    b.HasOne("EcommerceApp.Models.User", "UserCreate")
                        .WithMany()
                        .HasForeignKey("user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserCreate");
                });

            modelBuilder.Entity("EcommerceApp.Models.CartItem", b =>
                {
                    b.HasOne("EcommerceApp.Models.Cart", "Cart")
                        .WithMany()
                        .HasForeignKey("cart_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EcommerceApp.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("product_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cart");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("EcommerceApp.Models.Comment", b =>
                {
                    b.HasOne("EcommerceApp.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("product_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EcommerceApp.Models.User", "UserCreate")
                        .WithMany()
                        .HasForeignKey("user_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("UserCreate");
                });

            modelBuilder.Entity("EcommerceApp.Models.Product", b =>
                {
                    b.HasOne("EcommerceApp.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("category_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });
#pragma warning restore 612, 618
        }
    }
}
