using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace OrderWebApplication.Entity.Models
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options ) : base(options)
        {
        }
        public AppDbContext(string connectionString) : base(new DbContextOptionsBuilder<AppDbContext>().UseSqlServer(connectionString).Options)
        {

            string DatabaseConnection = "Server=UIGLT002\\SQLSERVER2019;Database=OrderManagement;Trusted_Connection=True;MultipleActiveResultSets=true;";
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);

            builder.Entity<ClaimStore>().ToTable("tbl_ClaimStore");
            builder.Entity<ClaimStore>().HasKey(j => j.ClaimId);

            builder.Entity<Purchase>(entity =>
            {
                entity.HasKey(e => e.PurchaseId);

                entity.ToTable("tbl_Purchase");

                entity.Property(e => e.PurchaseId)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.ProductName)
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.HasOne(d => d.PurchaseDetailIsdNavigation)
                    .WithMany(p => p.Purchases)
                    .HasForeignKey(d => d.PurchaseDetailsId)
                    .HasConstraintName("FK_tbl_Purchase_tbl_PurchaseDetails");
            });

            builder.Entity<PurchaseDetail>(entity =>
            {
                entity.HasKey(e => e.PurchaseDetailsId);

                entity.ToTable("tbl_PurchaseDetails");

                entity.Property(e => e.AdminUserId).HasMaxLength(450);

                entity.Property(e => e.PurchaseDatetime).HasColumnType("datetime");

                entity.Property(e => e.PurchaseDetailsUniqueId)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }



        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ClaimStore> ClaimStores { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<PurchaseDetail> PurchaseDetails { get; set; }

        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        public DbSet<Order> Orders { get; set; }



    }
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer("Server=UIGLT002\\SQLSERVER2019;Database=OrderManagement;Trusted_Connection=True;MultipleActiveResultSets=true;");

            return new AppDbContext(optionsBuilder.Options);
        }
    }

}