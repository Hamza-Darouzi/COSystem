

namespace COSystem.EF.Data.Config;

internal class TransactionConfig : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn().ValueGeneratedOnAdd().IsRequired();
        builder.Property(x => x.Quantity).HasColumnType("INT").IsRequired();
        builder.Property<DateOnly>(x => x.TransactionDate)
               .HasConversion<DateOnlyConverter, DateOnlyComparer>()
               .HasColumnType("date");
        builder.ToTable(nameof(Transaction));

        builder.HasOne(x => x.Product);


        // Relationships

        builder.HasOne(x => x.ProductionBranch)
               .WithMany(x => x.Transactions)
               .HasForeignKey(x => x.ProductionBranchId)
               .OnDelete(DeleteBehavior.NoAction)
               .IsRequired();

        builder.HasOne(x => x.DistributionBranch)
             .WithMany(x => x.Transactions)
             .HasForeignKey(x => x.DistributionBranchId)
              .OnDelete(DeleteBehavior.NoAction)
             .IsRequired();

        

       
    }
}
