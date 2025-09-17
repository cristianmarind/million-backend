using Million.Domain.Entities;

namespace Million.Application.Interfaces;

public class OwnerFilterOptions
{
    public required List<string> OwnerIdList { get; set; }
}
public interface IOwnerRepository
{
    Task<IEnumerable<Owner>> GetByFilterAsync(OwnerFilterOptions options);
}