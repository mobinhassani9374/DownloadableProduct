using DownloadableProduct.Domain.Entities;
using DownloadableProduct.Identity.DataModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DownloadableProduct.Identity
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
                .Property(c => c.FullName)
                .HasMaxLength(500)
                .IsRequired(true);

            var product = builder.Entity<Product>();

            product.Property(c => c.Description).HasMaxLength(500).IsRequired(true);
            product.Property(c => c.Title).HasMaxLength(100).IsRequired(true);
            product.Property(c => c.Dimensions).HasMaxLength(300).IsRequired(false);
            product.Property(c => c.Extension).HasMaxLength(100).IsRequired(false);
            product.Property(c => c.File).HasMaxLength(60).IsRequired(false);
            product.Property(c => c.Image).HasMaxLength(60).IsRequired(false);
            product.Property(c => c.SmallImage).HasMaxLength(60).IsRequired(false);
            product.Property(c => c.UserId).HasMaxLength(60).IsRequired(true);
            product.Property(c => c.UserUpoadImage).HasMaxLength(60).IsRequired(false);

            var purchase = builder.Entity<Purchase>();

            purchase.Property(c => c.UserId).HasMaxLength(60).IsRequired(true);

            purchase.
                HasOne(c => c.Product)
                .WithMany(c => c.Purchases)
                .HasForeignKey(c => c.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
