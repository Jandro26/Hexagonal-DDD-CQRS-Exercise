using Hexagonal_Exercise.catalog.category.domain;
using Hexagonal_Exercise.catalog.product.domain;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace Hexagonal_Exercise.core.infrastructure.entityFramework
{
    public class DataDbContext: DbContext
    {
        private static readonly string connectionString = ConfigurationManager.ConnectionStrings["Data:ConnectionString"].ToString();
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Category { get; set; }

        public DataDbContext() : base(GetOptions())
        {
        }

        private static DbContextOptions GetOptions()
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString).Options;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .ToTable("Product");

            modelBuilder.Entity<Category>()
                .ToTable("Category");
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
