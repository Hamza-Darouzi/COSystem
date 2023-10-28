


namespace COSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CompaniesController : ControllerBase
{
    private readonly IUnitOfWork _unit;
    private readonly IMapper _mapper;

    public CompaniesController(IUnitOfWork unit , IMapper mapper)
    {
        _unit = unit;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("/api/Companies/GetAllCompanies")]
    public async Task<IActionResult> GetAllCompanies()
    {
        var res = await _unit.Companies.GetAllAsync();
        if(res.Count() ==0) return NoContent();
        return Ok(res);
    }
    [HttpGet]
    [Route("/api/Companies/GetCompany")]
    public async Task<IActionResult> GetCompany(int companyId)
    {
        var res = await _unit.Companies.FindAsync(x=>x.Id == companyId);
        if (res is null) return BadRequest("Invalid Id");
        return Ok(res);
    }
    [HttpGet]
    [Route("/api/Companies/GetCompanyWithBranchesAndProducts")]
    public async Task<IActionResult> GetCompanyWithBranchesAndProducts(int companyId)
    {
        var res = await _unit.Companies.FindAsync(x => x.Id == companyId, new[]{ "ProductionBranches", "DistributionBranches","Products" });
        if (res is null) return BadRequest("Invalid Id");
        return Ok(res);
    }

    [HttpPost]
    [Route("/api/Companies/AddCompany")]
    public async Task<IActionResult> Create(CompanyDTO Request)
    {
        if (Request is null) return BadRequest("Invalid Input");
        var company = _mapper.Map<Company>(Request);
        await _unit.Companies.Create(company);
        await _unit.Complete();
        return Ok(company);
    }

    [HttpDelete]
    [Route("/api/Companies/DeleteCompany")]
    public async Task<IActionResult> Delete(int companyId)
    {
        var company = await _unit.Companies.FindAsync(x=>x.Id== companyId);
        if (company is null) return BadRequest("Invalid Id");
              _unit.Companies.Delete(company);
        await _unit.Complete();
        return Ok(company);
    }

    [HttpPut]
    [Route("/api/Companies/UpdateCompany")]
    public async Task<IActionResult> Update(CompanyDTO model,int companyId)
    {
        var Company = await _unit.Companies.FindAsync(x => x.Id == companyId);
        if (Company is null) return BadRequest("Invalid Id");
        Company.Name = model.Name;
        Company.Activity = model.Activity;
        Company.FoundingDate = model.FoundingDate;
        _unit.Companies.Update(Company);
        await _unit.Complete();
        return Ok();
    }

}
