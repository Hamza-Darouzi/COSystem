

namespace COSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DistributionBranchesController : ControllerBase
{
    private readonly IUnitOfWork _unit;
    private readonly IMapper _mapper;

    public DistributionBranchesController(IUnitOfWork unit , IMapper mapper)
    {
        _unit = unit;
        _mapper = mapper;
    }


    [HttpGet]
    [Route("/api/DistributionBranches/GetAll")]
    public async Task<IActionResult> GetAll()
    {
        var res = await _unit.DistributionBranches.GetAllAsync();
        if (res.Count() == 0) return NoContent();
        return Ok(res);

    }

    [HttpPost]
    [Route("/api/DistributionBranches/Add")]
    public async Task<IActionResult> Create(DistributionBranchDTO Request)
    {
        if (Request is null) return BadRequest("Invalid Id");
        var distributionBranch = _mapper.Map<DistributionBranch>(Request);
        await _unit.DistributionBranches.Create(distributionBranch);
        await _unit.Complete();
        return Ok(distributionBranch);
    }

    [HttpDelete]
    [Route("/api/DistributionBranches/Delete")]
    public async Task<IActionResult> Delete(int id)
    {
        var distributionBranches = await _unit.DistributionBranches.FindAsync(x => x.Id == id);
        if (distributionBranches is null) return BadRequest("Invalid Id");
        _unit.DistributionBranches.Delete(distributionBranches);
        await _unit.Complete();
        return Ok(distributionBranches);
    }
    [HttpPut]
    [Route("/api/DistributionBranches/UpdateDistributionBranch")]
    public async Task<IActionResult> Update(DistributionBranchDTO model, int branchId)
    {
        var distributionBranch = await _unit.DistributionBranches.FindAsync(x => x.Id == branchId);
        if (distributionBranch is null) return BadRequest("Invalid Input");
        distributionBranch.Name = model.Name;
        distributionBranch.Address = model.Address;
        distributionBranch.CompanyId = model.CompanyId;
        _unit.DistributionBranches.Update(distributionBranch);
        await _unit.Complete();
        return Ok();
    }
}
