﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Vk_server;

namespace Vk_server.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20200620195705_SizeModification2")]
    partial class SizeModification2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Vk_server.Clothing", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("About");

                    b.Property<string>("Link");

                    b.Property<string>("Name");

                    b.Property<string>("Picture");

                    b.Property<decimal>("Price");

                    b.Property<long>("SexId");

                    b.Property<long>("ShopId");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.HasIndex("ShopId");

                    b.ToTable("Clothings");
                });

            modelBuilder.Entity("Vk_server.ClothingSize", b =>
                {
                    b.Property<long>("ClothingId");

                    b.Property<long>("SizeId");

                    b.HasKey("ClothingId", "SizeId");

                    b.HasIndex("SizeId");

                    b.ToTable("ClothingSize");
                });

            modelBuilder.Entity("Vk_server.OAuthClientDetail", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AccessToken");

                    b.Property<string>("Display");

                    b.Property<DateTime?>("ExpiresIn");

                    b.Property<string>("RefreshToken");

                    b.Property<string>("Scope");

                    b.HasKey("Id");

                    b.ToTable("OAuthClientDetails");
                });

            modelBuilder.Entity("Vk_server.PhotoHuman", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("PhotoPath");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("PhotoHumans");
                });

            modelBuilder.Entity("Vk_server.RenderPhoto", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("ClothingId");

                    b.Property<string>("PhotoPath");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ClothingId");

                    b.HasIndex("UserId");

                    b.ToTable("RenderPhotos");
                });

            modelBuilder.Entity("Vk_server.Sex", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Sexs");
                });

            modelBuilder.Entity("Vk_server.Shop", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("About");

                    b.Property<string>("Logo");

                    b.Property<string>("Name");

                    b.Property<string>("Url");

                    b.HasKey("Id");

                    b.ToTable("Shops");
                });

            modelBuilder.Entity("Vk_server.Size", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("SizeName");

                    b.Property<string>("SizesType");

                    b.HasKey("Id");

                    b.ToTable("Sizes");
                });

            modelBuilder.Entity("Vk_server.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName");

                    b.Property<long>("Height");

                    b.Property<bool>("IsSizeRight");

                    b.Property<string>("LastName");

                    b.Property<long?>("OAuthClientDetailId");

                    b.Property<long>("SexId");

                    b.Property<long?>("SizeId");

                    b.Property<long?>("UserParameterId");

                    b.Property<long?>("VkId");

                    b.HasKey("Id");

                    b.HasIndex("OAuthClientDetailId");

                    b.HasIndex("SexId");

                    b.HasIndex("UserParameterId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Vk_server.UserParameter", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Chest");

                    b.Property<double>("Hips");

                    b.Property<double>("Legs");

                    b.Property<string>("PartsCoordinates");

                    b.Property<long?>("SizeDownId");

                    b.Property<long?>("SizeMiddleId");

                    b.Property<long?>("SizeUpId");

                    b.Property<long>("UserId");

                    b.Property<double>("Waist");

                    b.HasKey("Id");

                    b.HasIndex("SizeDownId");

                    b.HasIndex("SizeMiddleId");

                    b.HasIndex("SizeUpId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UserParameters");
                });

            modelBuilder.Entity("Vk_server.Clothing", b =>
                {
                    b.HasOne("Vk_server.Shop", "Shop")
                        .WithMany()
                        .HasForeignKey("ShopId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Vk_server.ClothingSize", b =>
                {
                    b.HasOne("Vk_server.Clothing", "Clothing")
                        .WithMany()
                        .HasForeignKey("ClothingId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Vk_server.Size", "Size")
                        .WithMany()
                        .HasForeignKey("SizeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Vk_server.PhotoHuman", b =>
                {
                    b.HasOne("Vk_server.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Vk_server.RenderPhoto", b =>
                {
                    b.HasOne("Vk_server.Clothing", "Clothing")
                        .WithMany()
                        .HasForeignKey("ClothingId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Vk_server.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Vk_server.User", b =>
                {
                    b.HasOne("Vk_server.OAuthClientDetail", "OAuthClientDetail")
                        .WithMany()
                        .HasForeignKey("OAuthClientDetailId");

                    b.HasOne("Vk_server.Sex", "Sex")
                        .WithMany()
                        .HasForeignKey("SexId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Vk_server.UserParameter", "UserParameter")
                        .WithMany()
                        .HasForeignKey("UserParameterId");
                });

            modelBuilder.Entity("Vk_server.UserParameter", b =>
                {
                    b.HasOne("Vk_server.Size", "SizeDown")
                        .WithMany()
                        .HasForeignKey("SizeDownId");

                    b.HasOne("Vk_server.Size", "SizeMiddle")
                        .WithMany()
                        .HasForeignKey("SizeMiddleId");

                    b.HasOne("Vk_server.Size", "SizeUp")
                        .WithMany()
                        .HasForeignKey("SizeUpId");

                    b.HasOne("Vk_server.User", "User")
                        .WithOne()
                        .HasForeignKey("Vk_server.UserParameter", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
