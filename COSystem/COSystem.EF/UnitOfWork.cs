



namespace Football.EF;

public class UnitOfWork:IUnitOfWork
{
    private readonly AppDbContext _context;
    public IBaseService<Company> Companies { get; private set ; }
    public IBaseService<DistributionBranch> DistributionBranches { get; private set; }
    public IBaseService<ProductionBranch> ProductionBranches { get; private set; }
    public IBaseService<Production> Productions { get; private set; }
    public IBaseService<Transaction> Transactions { get; private set; }
    public IBaseService<Product> Products { get; private set; }


    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Companies = new BaseRepository<Company>(_context);
        DistributionBranches = new BaseRepository<DistributionBranch>(_context);
        ProductionBranches = new BaseRepository<ProductionBranch>(_context);
        Productions = new BaseRepository<Production>(_context);
        Transactions = new BaseRepository<Transaction>(_context);
    }

    public async Task<int>Complete()
    {
       return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
  

}
