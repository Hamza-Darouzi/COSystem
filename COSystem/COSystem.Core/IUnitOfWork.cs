


using COSystem.Core.Models;
using COSystem.Core.Services;

namespace COSystem.Core;

public interface IUnitOfWork:IDisposable
{
    IBaseService<Company> Companies { get; }
    IBaseService<DistributionBranch> DistributionBranches { get; }
    IBaseService<ProductionBranch> ProductionBranches { get; }
    IBaseService<Production> Productions { get; }
    IBaseService<Transaction> Transactions { get; }
    IBaseService<Product> Products { get; }
    Task<int> Complete();
}
