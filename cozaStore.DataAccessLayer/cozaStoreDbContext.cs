using cozaStore.Models;
using System;
using System.Data.Entity;

namespace cozaStore.DataAccessLayer
{
    public class cozaStoreDbContext : DbContext
    {
        public cozaStoreDbContext() : base("name=cozaStoreDb") { }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<Comment> Comments { get; set; }

        public virtual DbSet<Contact> Contacts { get; set; }

        public virtual DbSet<Coupon> Coupons { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<OrderDetail> OrderDetails { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<Role> Roles { get; set; }

        public virtual DbSet<Status> Statuses { get; set; }

        public virtual DbSet<Supplier> Suppliers { get; set; }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //setdatetime

            modelBuilder.Properties<DateTime>().Configure(c => c.HasColumnType("datetime2"));

            //set varchar 
            modelBuilder.Entity<Comment>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Contact>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.PassWord)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Phone)
                .IsUnicode(false);
            modelBuilder.Entity<Supplier>()
               .Property(e => e.Phone)
               .IsUnicode(false);

            //set decimal product
            modelBuilder.Entity<Product>()
               .Property(e => e.Price)
               .HasPrecision(18, 0);

            //Category with product one more
            modelBuilder.Entity<Category>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.Category)
                .WillCascadeOnDelete(false);

            //Supplier with product one more
            modelBuilder.Entity<Supplier>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.Supplier)
                .WillCascadeOnDelete(false);


            //Order with OrderDetail
            modelBuilder.Entity<Order>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.Order)
                .WillCascadeOnDelete(false);

            //product with orderDetails
            modelBuilder.Entity<Product>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            //product with comment
            modelBuilder.Entity<Product>()
                .HasMany(e => e.Comments)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);


            //Role with User
            modelBuilder.Entity<Role>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.Role)
                .WillCascadeOnDelete(false);

            //Coupon with Order
            modelBuilder.Entity<Coupon>()
                .HasMany(e => e.Orders)
                .WithRequired(e => e.Coupon)
                .WillCascadeOnDelete(false);
        }
    }
}
