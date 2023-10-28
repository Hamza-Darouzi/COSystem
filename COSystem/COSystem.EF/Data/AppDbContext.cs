

namespace COSystem.EF.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options)
    {

    }
    public DbSet<Company> Companies { get; set; }
    public DbSet<ProductionBranch> ProductionBranches { get; set; }
    public DbSet<DistributionBranch> DistributionBranches { get; set; }
    public DbSet<Production> Productions { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<Product> Products { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
