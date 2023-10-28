namespace COSystem.Core.DTOs;

public class CompanyDTO
{
    public string Name { get; set; } = string.Empty;
    public string Activity { get; set; } = string.Empty;

    public DateOnly FoundingDate { get; set; }
}
