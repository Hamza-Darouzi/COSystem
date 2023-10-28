
namespace COSystem.Core.Models;

public class Transaction
{
    public int Id { get; set; }
    public int DistributionBranchId { get; set; }
    [JsonIgnore]
    public DistributionBranch DistributionBranch { get; set; } = null!;
    public int ProductionBranchId { get; set; }
    [JsonIgnore]
    public ProductionBranch ProductionBranch { get; set; } = null!;
    public int ProductId { get; set; }
    [JsonIgnore]
    public Product Product { get; set; } = null!;

    public int Quantity { get; set; }
    public DateOnly TransactionDate { get; set; } 
}
