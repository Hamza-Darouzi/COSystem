using AutoMapper;


namespace COSystem.EF.Data.AutoMapperConfig
{
    internal class MapperProfile:Profile
    {
        public MapperProfile()
        {

            CreateMap<CompanyDTO, Company>()
                .ForMember(dist => dist.Name, src => src.MapFrom(x => x.Name))
                .ForMember(dist => dist.FoundingDate, src => src.MapFrom(x => x.FoundingDate))
                .ForMember(dist => dist.Activity, src => src.MapFrom(x => x.Activity))
                .ReverseMap();

            CreateMap<ProductionBranchDTO, ProductionBranch>()
                .ForMember(dist => dist.Name, src => src.MapFrom(x => x.Name))
                .ForMember(dist => dist.Address, src => src.MapFrom(x => x.Address))
                .ForMember(dist => dist.CompanyId, src => src.MapFrom(x => x.CompanyId))
                .ReverseMap();

            CreateMap<DistributionBranchDTO, DistributionBranch>()
                .ForMember(dist => dist.Name, src => src.MapFrom(x => x.Name))
                .ForMember(dist => dist.Address, src => src.MapFrom(x => x.Address))
                .ForMember(dist => dist.CompanyId, src => src.MapFrom(x => x.CompanyId))
                .ReverseMap();

            CreateMap<ProductionsDTO, Production>()
               .ForMember(dist => dist.ProductionBranchId, src => src.MapFrom(x => x.ProductionBranchId))
               .ForMember(dist => dist.ProductionDate, src => src.MapFrom(x => x.ProductionDate))
               .ForMember(dist => dist.Quantity, src => src.MapFrom(x => x.Quantity))
               .ForMember(dist => dist.ProductId, src => src.MapFrom(x => x.ProductId))
               .ReverseMap();

            CreateMap<TransactionDTO, Transaction>()
               .ForMember(dist => dist.ProductionBranchId, src => src.MapFrom(x => x.ProductionBranchId))
               .ForMember(dist => dist.DistributionBranchId, src => src.MapFrom(x => x.DistributionBranchId))
               .ForMember(dist => dist.TransactionDate, src => src.MapFrom(x => x.TransactionDate))
               .ForMember(dist => dist.Quantity, src => src.MapFrom(x => x.Quantity))
               .ForMember(dist => dist.ProductId, src => src.MapFrom(x => x.ProductId))
               .ReverseMap();

            CreateMap<ProductDTO, Product>()
               .ForMember(dist => dist.Name, src => src.MapFrom(x => x.Name))
               .ForMember(dist => dist.Type, src => src.MapFrom(x => x.Type))
               .ReverseMap();
        }

    }
}
