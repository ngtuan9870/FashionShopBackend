using Microsoft.EntityFrameworkCore;

namespace FashionShopNETCoreAPI.Data
{
    public class FashionShopDBContext : DbContext
    {
        public FashionShopDBContext(DbContextOptions options):base(options) { }

        #region DbSet
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>(e =>
            {
                e.ToTable("Products");
                e.HasKey(p => p.product_id);
                e.Property(p => p.product_name).HasMaxLength(100);
                e.HasOne(p => p.Category).WithMany(p => p.Products).HasForeignKey(p => p.category_id)
                .HasConstraintName("FK_Product_Category");
            });
            modelBuilder.Entity<Category>(e =>
            {
                e.ToTable("Categories");
                e.HasKey(c => c.category_id);
                e.Property(c => c.category_name).HasMaxLength(100).IsRequired();
            });
            modelBuilder.Entity<User>(e =>
            {
                e.ToTable("Users");
                e.HasKey(u => u.user_id);
                e.Property(u => u.user_name).HasMaxLength(100).IsRequired();
                e.Property(u => u.user_password).IsRequired();
                e.Property(u => u.user_email).IsRequired();
                e.Property(u => u.user_birthday).HasDefaultValueSql("getutcdate()").IsRequired();
            });
        }
    }
}
