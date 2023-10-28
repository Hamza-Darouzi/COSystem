

namespace COSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductionBranchesController : ControllerBase
{
    private readonly IUnitOfWork _unit;
    private readonly IMapper _mapper;

    public ProductionBranchesController(IUnitOfWork unit,IMapper mapper)
    {
        _unit = unit;
        _mapper = mapper;
    }


    [HttpGet]
    [Route("/api/ProductionnBranches/GetCompanyProductionBranches")]
    public async Task<IActionResult> GetCompanyProductionBranches(int companyId)
    {
        var res = await _unit.ProductionBranches.FindAsync(x=>x.CompanyId == companyId);
        if (res is null) return NoContent();
        return Ok(res);
    }

    [HttpGet]
    [Route("/api/ProductionBranches/GetProductionBranchesWithProductionOperations")]
    public async Task<IActionResult> Get(int branchId)
    {
        var res = await _unit.ProductionBranches.FindAsync(x => x.Id == branchId, new[] {"Productions"});
        if (res is null) return BadRequest("Invalid Id");
        return Ok(res);
    }

    [HttpPost]
    [Route("/api/ProductionBranches/Add")]
    public async Task<IActionResult> Create(ProductionBranchDTO Request)
    {
        if (Request is null) return BadRequest("Invalid Input");
        var productionBranch = _mapper.Map<ProductionBranch>(Request);
        await _unit.ProductionBranches.Create(productionBranch);
        await _unit.Complete();
        return Ok(productionBranch);
    }

    [HttpDelete]
    [Route("/api/ProductionBranches/Delete")]
    public async Task<IActionResult> Delete(int id)
    {
        var productionBranches = await _unit.ProductionBranches.FindAsync(x => x.Id == id);
        if (productionBranches is null ) return BadRequest("Invalid Id" );
        _unit.ProductionBranches.Delete(productionBranches);
        await _unit.Complete();
        return Ok(productionBranches);
    }

    [HttpPut]
    [Route("/api/ProductionBranches/UpdateProductionBranch")]
    public async Task<IActionResult> Update(ProductionBranchDTO model, int branchId)
    {
        var productionBranch = await _unit.ProductionBranches.FindAsync(x => x.Id == branchId);
        if (productionBranch is null) return BadRequest("Invalid Id");
        productionBranch.Name = model.Name;
        productionBranch.Address = model.Address;
        productionBranch.CompanyId = model.CompanyId;
        _unit.ProductionBranches.Update(productionBranch);
        await _unit.Complete();
        return Ok();
    }
}
