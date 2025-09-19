using Million.Application.DTOs;
using Million.Application.Common;

namespace Million.Application.Interfaces;

public interface IOwnerService
{
    Task<Result<IEnumerable<OwnerDto>>> GetOwnersByFilterAsync(OwnerFilterOptions options);
}
