using Million.Application.Common;
using Million.Application.DTOs;
using Million.Application.Interfaces;

namespace Million.Application.Features.Owners.Services;

public class OwnerService : IOwnerService {
    private readonly IOwnerRepository _repository;

    public OwnerService(IOwnerRepository repository) {
        _repository = repository;
    }

    public async Task<Result<IEnumerable<OwnerDto>>> GetOwnersByFilterAsync(OwnerFilterOptions options) {
        var owners = await _repository.GetByFilterAsync(options);

        if (owners == null || !owners.Any()) {
            return Result<IEnumerable<OwnerDto>>.Fail("No owners found with the given filter options.");
        }

        return Result<IEnumerable<OwnerDto>>.Ok(owners.Select(p => new OwnerDto {
            Name = p.Name,
            Address = p.Address,
            Photo = p.Photo,
            Birthday = p.Birthday
        }));
    }
}