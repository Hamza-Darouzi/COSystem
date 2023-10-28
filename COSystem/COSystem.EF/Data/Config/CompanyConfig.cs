

namespace COSystem.EF.Data.Config;

internal class CompanyConfig : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn().ValueGeneratedOnAdd().IsRequired();
        builder.Property(x=>x.Name).HasColumnType("VARCHAR").HasMaxLength(100).IsRequired();
        builder.Property(x=>x.Activity).HasColumnType("VARCHAR").HasMaxLength(100).IsRequired(false);
        builder.Property<DateOnly>(x => x.FoundingDate)
               .HasConversion<DateOnlyConverter, DateOnlyComparer>()
               .HasColumnType("date");
        builder.ToTable(nameof(Company));


        //Relationships
        builder.HasMany(x=>x.DistributionBranches)
               .WithOne(x => x.Company)
               .HasForeignKey(x=>x.CompanyId)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired();
        builder.HasMany(x => x.ProductionBranches)
               .WithOne(x => x.Company)
               .HasForeignKey(x => x.CompanyId)
               .OnDelete(DeleteBehavior.Cascade)
               .IsRequired();
        builder.HasMany(x => x.Products);


    }
}
