
namespace COSystem.Core.Models;

public class DistributionBranch
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int CompanyId { get; set; }
    [JsonIgnore]
    public Company Company { get; set; } = null!;
    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

}
