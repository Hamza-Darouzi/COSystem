
namespace COSystem.Core.Models;

public class Company
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Activity { get; set; } = string.Empty;
    public DateOnly FoundingDate { get; set; }

    public ICollection<DistributionBranch> DistributionBranches { get; set; }= new List<DistributionBranch>();
    public ICollection<ProductionBranch> ProductionBranches { get; set; }= new List<ProductionBranch>();
    public ICollection<Product> Products { get; set; }= new List<Product>();
}
