using Million.Application.DTOs;

namespace Million.Application.Interfaces;

public interface IOwnerService
{
    Task<IEnumerable<OwnerDto>> GetOwnersByFilterAsync(OwnerFilterOptions options);
}
