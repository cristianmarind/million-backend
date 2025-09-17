using Million.Domain.Entities;

namespace Million.Application.Interfaces;

public class PropertyFilterOptions
{
    public string? Name { get; set; }
    public string? Address { get; set; }
    public decimal? MinPrice { get; set; }
    public decimal? MaxPrice { get; set; }
    public int? Page { get; set; } = 1;
    public int? PageSize { get; set; } = 10;
}
public interface IPropertyRepository
{
    Task<IEnumerable<Property>> GetByFilterAsync(PropertyFilterOptions options);
}