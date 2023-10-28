


namespace COSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransactionController : ControllerBase
{
    private readonly IUnitOfWork _unit;
    private readonly IMapper _mapper;

    public TransactionController(IUnitOfWork unit,IMapper mapper)
    {
        _unit = unit;
        _mapper = mapper;
    }


    [HttpGet]
    [Route("api/Transactions/GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var res = await _unit.Transactions.GetAllAsync();
        if (res.Count() == 0) return NoContent();

        return Ok(res);
    }

    [HttpGet]
    [Route("api/Transactions/Get")]
    public async Task<IActionResult> Get(int id)
    {
        var res = await _unit.Transactions.FindAsync(x=>x.Id==id);
        if (res is null) return BadRequest("Invalid Id");
        return Ok(res);
    }

    [HttpGet]
    [Route("api/Transactions/GetCompanyTransactions")]
    public async Task<IActionResult> GetCompanyTransactions(int companyId)
    {
        var branches = await _unit.ProductionBranches.FindAsync(x => x.CompanyId == companyId, new[] {"Transactions"});
        var res = branches.Transactions.ToList();
        if (res is null) return BadRequest("Invalid Id");
        return Ok(res);
    }

    [HttpGet]
    [Route("/api/Transactions/GetReport")]
    public async Task<IActionResult> GetReport(int Companyid , DateOnly date1 , DateOnly date2 )
    {

        var branches = await _unit.ProductionBranches.FindAllAsync(x => x.CompanyId == Companyid, new[] {"Productions"});
        var ProductionsReport = branches.Select( x => x.Transactions.Where(
                                   d => d.TransactionDate >= date1 && d.TransactionDate <= date2
                                   ));
        if (ProductionsReport is null) return BadRequest("Invalid Input");

        return Ok(ProductionsReport);
    }

    [HttpPost]
    [Route("api/Transactions/Add")]
    public async Task<IActionResult> Create(TransactionDTO Request)
    {
        if (Request is null) return BadRequest("Invalid Input");
        var prodbranch = await _unit.ProductionBranches.FindAsync(x => x.Id == Request.ProductionBranchId);
        var company = await _unit.Companies.FindAsync(x => x.Id == prodbranch.CompanyId);
        var isProductAvailable = company.Products.Any(x => x.Id == Request.ProductId);
        if (isProductAvailable) return BadRequest($"Product is not Availabe in {company.Name} Stores");
        var transaction = _mapper.Map<Transaction>(Request);
        await _unit.Transactions.Create(transaction);
        await _unit.Complete();
        return Ok(transaction);
    }

    [HttpDelete]
    [Route("/api/Transactions/Delete")]
    public async Task<IActionResult> Delete(int id)
    {
        if (id <= 0) return BadRequest("Invalid Id");
        var transaction = await _unit.Transactions.FindAsync(x => x.Id == id);
        _unit.Transactions.Delete(transaction);
        await _unit.Complete();
        return Ok(transaction);
    }

    [HttpPut]
    [Route("/api/Transactions/UpdateTransaction")]
    public async Task<IActionResult> Update(TransactionDTO model, int TranId)
    {
        var transactions = await _unit.Transactions.FindAsync(x => x.Id == TranId);
        if (transactions is null) return BadRequest("Invalid Id");
        transactions.ProductionBranchId = model.ProductionBranchId;
        transactions.DistributionBranchId = model.DistributionBranchId;
        transactions.TransactionDate = model.TransactionDate;
        transactions.Quantity = model.Quantity;
        _unit.Transactions.Update(transactions);
        await _unit.Complete();
        return Ok();
    }
}
