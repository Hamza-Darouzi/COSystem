

namespace COSystem.EF.Data.Config;

internal class ProductionConfig : IEntityTypeConfiguration<Production>
{
    public void Configure(EntityTypeBuilder<Production> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn().ValueGeneratedOnAdd().IsRequired();
        builder.Property(x => x.Quantity).HasColumnType("INT").IsRequired();
        builder.Property<DateOnly>(x => x.ProductionDate)
               .HasConversion<DateOnlyConverter, DateOnlyComparer>()
               .HasColumnType("date");
        builder.ToTable(nameof(Production));


        // RelationShip
        
        builder.HasOne(x => x.ProductionBranch)
               .WithMany(x => x.Productions)
               .HasForeignKey(x => x.ProductionBranchId)
               .OnDelete(DeleteBehavior.NoAction)
               .IsRequired();
        builder.HasOne(x => x.Product);
       
        
    }
}
