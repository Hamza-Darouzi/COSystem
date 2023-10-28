
namespace COSystem.Core.DTOs;

public class TransactionDTO
{
    public int ProductId { get; set; }
    public int ProductionBranchId { get; set; }
    public int DistributionBranchId { get; set; }
    public int Quantity { get; set; }
    public DateOnly TransactionDate { get; set; }
}
