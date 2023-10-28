

namespace COSystem.EF.Data.Config;

internal class ProductionBranchConfig : IEntityTypeConfiguration<ProductionBranch>
{
    public void Configure(EntityTypeBuilder<ProductionBranch> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn().ValueGeneratedOnAdd().IsRequired();
        builder.Property(x => x.Name).HasColumnType("VARCHAR").HasMaxLength(100).IsRequired();
        builder.Property(x => x.Address).HasColumnType("VARCHAR").HasMaxLength(100).IsRequired(false);

        builder.ToTable(nameof(ProductionBranch));
    }
}
