using Million.Application.DTOs;
using Million.Application.Interfaces;
using Million.Domain.Entities;

namespace Million.Application.Features.Properties.Services;

public class PropertyService {
    private readonly IPropertyRepository _repository;

    public PropertyService(IPropertyRepository repository) {
        _repository = repository;
    }

    public async Task<IEnumerable<PropertyDto>> GetPropertiesByFilterAsync(
        PropertyFilterOptions options
    ) {
        var properties = await _repository.GetByFilterAsync(options);

        return properties.Select(p => new PropertyDto {
            Id = p.Id,
            Name = p.Name,
            Address = $"{p.Address.Street}, {p.Address.City}, {p.Address.Country}",
            Price = p.Price.Amount,
            CodeInternal = p.CodeInternal,
            Year = p.Year,
            OwnerId = p.OwnerId,
            ImageUrls = p.PropertyImages.Select(pi => pi.File).ToList(),
            PropertyTraces = p.PropertyTraces.Select(pt => new PropertyTraceDto {
                Date = pt.DateSale,
                Name = pt.Name,
                Value = pt.Value,
                Tax = pt.Tax
            }).ToList(),
            PresentationConfig = new PresentationConfigDto {
                CoverImageIndex = p.PresentationConfig.CoverImageIndex,
                ListClass = p.PresentationConfig.ListClass
            }
        });
    }
}