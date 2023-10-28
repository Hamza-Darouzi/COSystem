

namespace COSystem.Core.Models;

public class ProductionBranch 
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public int CompanyId { get; set; }
    [JsonIgnore]
    public Company Company { get; set; } = null!;
    public ICollection<Production> Productions { get; set; } = new List<Production>();
    public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
}
