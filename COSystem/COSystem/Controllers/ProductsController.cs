

namespace COSystem.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IUnitOfWork _unit;
    private readonly IMapper _mapper;

    public ProductsController(IUnitOfWork unit , IMapper mapper)
    {
        _unit = unit;
        _mapper = mapper;
    }



    [HttpPost]
    [Route("api/Products/AddCompanyProduct")]
    public async Task<IActionResult> AddCompanyProduct(int companyId,ProductDTO Request)
    {
        var company = await _unit.Companies.FindAsync(x=>x.Id== companyId);
        if(company is null) return BadRequest("Invalid Company Id");
        if(Request is null) return BadRequest("Invalid Product Data");
        var product = _mapper.Map<Product>(Request);
        company.Products.Add(product);
        await _unit.Complete();
        return Ok(product);
    }
    [HttpDelete]
    [Route("/api/Products/DeleteProduct")]
    public async Task<IActionResult> Delete(int productId)
    {
        var product = await _unit.Products.FindAsync(x => x.Id == productId);
        if (product is null) return BadRequest("Invalid Id");
        _unit.Products.Delete(product);
        await _unit.Complete();
        return Ok(product);
    }

    [HttpPut]
    [Route("/api/Products/UpdateProduct")]
    public async Task<IActionResult> Update(ProductDTO model, int productId)
    {
        var product = await _unit.Products.FindAsync(x => x.Id == productId);
        if (product is null) return BadRequest("Invalid Id");
        product.Name = model.Name;
        product.Type = model.Type;
        _unit.Products.Update(product);
        await _unit.Complete();
        return Ok();
    }

}
