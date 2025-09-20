using Million.Application.DTOs;
using Million.Application.Common;

namespace Million.Application.Interfaces;

public interface IPropertyService {
    Task<Result<IEnumerable<PropertyDto>>> GetPropertiesByFilterAsync(PropertyFilterOptions options);
}
