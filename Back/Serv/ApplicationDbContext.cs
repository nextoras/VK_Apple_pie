using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Vk_server
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<OAuthClientDetail> OAuthClientDetails { get; set; }
        public DbSet<PhotoHuman> PhotoHumans { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Clothing> Clothings { get; set; }
        public DbSet<Sex> Sexs { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<ClothingSize> ClothingSizes { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<RenderPhoto> RenderPhotos { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Basket>().HasKey(table => new
            {
                table.UserId,
                table.ClothingId
            });
        }
    }

    

    public class User
    {
        public long Id { get; set; }
        public long? VkId { get; set; }
        public long? OAuthClientDetailId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long SexId { get; set; }
        public long? SizeId { get; set; }
        public bool IsSizeRight { get; set; }

        [ForeignKey("OAuthClientDetailId")]
        public virtual OAuthClientDetail OAuthClientDetail { get; set; }

        [ForeignKey("SexId")]
        public virtual Sex Sex { get; set; }

        [ForeignKey("SizeId")]
        public virtual Size Size { get; set; }
    }

    public class OAuthClientDetail
    {
        public long Id { get; set; }
        public string AccessToken { get; set; }
        public string Scope { get; set; }

        public string RefreshToken { get; set; }
        public DateTime? ExpiresIn { get; set; }
        public string Display { get; set; }
    }

    public class PhotoHuman
    {
        public long Id { get; set; }
        public string PhotoPath { get; set; }
        public long UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public string Coordinates { get; set; }
    }

    public class RenderPhoto
    {
        public long Id { get; set; }
        public string PhotoPath { get; set; }
        public long UserId { get; set; }
        public long ClothingId { get; set; }

        [ForeignKey("ClothingId")]
        public virtual Clothing Clothing { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }

    public class Size
    {
        public long Id { get; set; }
        public double Chest { get; set; }
        public double Waist { get; set; }
        public double Hips { get; set; }
        public double Shulders { get; set; }
        public double Pelvic { get; set; }
        public double Legs { get; set; }
        public double Foots { get; set; }
        public long UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }

    public class Clothing
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long SexId { get; set; }
        public string Picture { get; set; }
        public string About { get; set; }
        public decimal Price { get; set; }
        public long? ClothingSizeId { get; set; }
        public string Type { get; set; }
        public string Link { get; set; }
        public long ShopId { get; set; }

        [ForeignKey("ClothingSizeId")]
        public virtual ClothingSize ClothingSize { get; set; }

        [ForeignKey("ShopId")]
        public virtual Shop Shop { get; set; }
    }

    public class Sex
    {
        public long Id { get; set; }
        public string Name { get; set; }
    }

    public class ClothingSize
    {
        public long Id { get; set; }
        public string SizeName { get; set; }
        public double? SizeN { get; set; }
    }

    public class Basket
    {
        public long UserId { get; set; }
        public long ClothingId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("ClothingId")]
        public virtual Clothing Clothing { get; set; }
        
    }

    public class Shop
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string About { get; set; }
        public string Logo { get; set; }
    }
}