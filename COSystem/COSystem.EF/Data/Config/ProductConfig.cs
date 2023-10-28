

namespace COSystem.EF.Data.Config
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn().ValueGeneratedOnAdd().IsRequired();
            builder.Property(x => x.Name).HasColumnType("VARCHAR").HasMaxLength(100).IsRequired();
            builder.Property(x => x.Type).HasColumnType("VARCHAR").HasMaxLength(100).IsRequired(false);
     
            builder.ToTable(nameof(Product));
        }
    }
}
