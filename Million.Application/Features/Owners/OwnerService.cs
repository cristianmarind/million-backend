using Million.Application.DTOs;
using Million.Application.Interfaces;

namespace Million.Application.Features.Owners.Services;

public class OwnerService {
    private readonly IOwnerRepository _repository;

    public OwnerService(IOwnerRepository repository) {
        _repository = repository;
    }

    public async Task<IEnumerable<OwnerDto>> GetOwnersByFilterAsync(OwnerFilterOptions options) {
        var owners = await _repository.GetByFilterAsync(options);

        return owners.Select(p => new OwnerDto {
            Name = p.Name,
            Address = p.Address,
            Photo = p.Photo,
            Birthday = p.Birthday
        });
    }
}