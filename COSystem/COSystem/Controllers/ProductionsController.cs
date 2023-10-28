


namespace COSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductionsController : ControllerBase
{
    private readonly IUnitOfWork _unit;
    private readonly IMapper _mapper;

    public ProductionsController(IUnitOfWork unit,IMapper mapper)
    {
        _unit = unit;
        _mapper = mapper;
    }
    
    [HttpGet]
    [Route("/api/Productions/GetCompanyProductions")]
    public async Task<IActionResult> Get(int companyId)
    {
        var ProdBranches = await _unit.ProductionBranches.FindAllAsync(x=>x.CompanyId== companyId, new[] {"Productions"});
        var prods = ProdBranches.Select(x => x.Productions);
        if (prods is null) return BadRequest("Invalid Id");
        return Ok(prods);
    }

    [HttpGet]
    [Route("/api/Productions/GetCompanyProductionsReport")]
    public async Task<IActionResult> GetReport(int Companyid , DateOnly date1 , DateOnly date2 )
    {

        var branches = await _unit.ProductionBranches.FindAllAsync(x => x.CompanyId == Companyid, new[] {"Productions"});
        var ProductionsReport = branches.Select( x => x.Productions.Where(
                                   d => d.ProductionDate >= date1 && d.ProductionDate <= date2
                                   ));

        if (ProductionsReport is null) return BadRequest("Invalid Id");
        return Ok(ProductionsReport);
    }


    [HttpPost]
    [Route("/api/Productions/AddProduction")]
    public async Task<IActionResult> Create(ProductionsDTO Request)
    {
        if (Request is null) return BadRequest("Invalid Input");
        var prodbranch = await _unit.ProductionBranches.FindAsync(x => x.Id == Request.ProductionBranchId);
        var company    = await _unit.Companies.FindAsync(x => x.Id == prodbranch.CompanyId);
        var isProductAvailable = company.Products.Any(x => x.Id == Request.ProductId);
        if (isProductAvailable) return BadRequest($"Product is not Availabe in {company.Name} Stores");
        var production = _mapper.Map<Production>(Request);
        await _unit.Productions.Create(production);
        await _unit.Complete();
        return Ok(production);
    }

    [HttpDelete]
    [Route("/api/Productions/DeleteProduction")]
    public async Task<IActionResult> Delete(int id)
    {
        var productions = await _unit.Productions.FindAsync(x => x.Id == id);
        if (productions is null) return BadRequest("Invalid Id");
        _unit.Productions.Delete(productions);
        await _unit.Complete();
        return Ok(productions);
    }

    [HttpPut]
    [Route("/api/Productions/UpdateProduction")]
    public async Task<IActionResult> Update(ProductionsDTO model, int ProdId)
    {
        var production = await _unit.Productions.FindAsync(x => x.Id == ProdId);
        if (production is null) return BadRequest("Invalid Id");
        production.ProductionBranchId = model.ProductionBranchId;
        production.ProductionDate = model.ProductionDate;
        production.Quantity = model.Quantity;
        _unit.Productions.Update(production);
        await _unit.Complete();
        return Ok();
    }
}
