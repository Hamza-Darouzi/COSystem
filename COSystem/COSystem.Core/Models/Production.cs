

namespace COSystem.Core.Models;

public class Production
{
    public int Id { get; set; }
    public int ProductionBranchId { get; set; }
    [JsonIgnore]
    public ProductionBranch ProductionBranch { get; set; } = null!;
  
    public int ProductId { get; set; }
    [JsonIgnore]
    public Product Product { get; set; } = null!;

    public int Quantity { get; set; } 
    public DateOnly ProductionDate { get; set; }
}
