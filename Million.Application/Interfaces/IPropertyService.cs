using Million.Application.DTOs;

namespace Million.Application.Interfaces;

public interface IPropertyService
{
    Task<IEnumerable<PropertyDto>> GetPropertiesByFilterAsync(PropertyFilterOptions options);
}
