using Million.Application.Interfaces;
using Million.Domain.Entities;
using Million.Domain.ValueObjects;
using MongoDB.Driver;

namespace MillionAPI.Repositories;

public class PropertyRepository : IPropertyRepository {
    private readonly IMongoCollection<PropertyDocument> _collection;

    public PropertyRepository(IMongoDatabase database) {
        _collection = database.GetCollection<PropertyDocument>("Properties");
    }

    private FilterDefinition<PropertyDocument> BuildFilter(PropertyFilterOptions options) {
        var filterBuilder = Builders<PropertyDocument>.Filter;
        var filter = filterBuilder.Empty;

        if (!string.IsNullOrWhiteSpace(options.Name))
            filter &= filterBuilder.Regex(p => p.Name, new MongoDB.Bson.BsonRegularExpression(options.Name, "i"));

        if (!string.IsNullOrWhiteSpace(options.Address))
            filter &= filterBuilder.Or(
                filterBuilder.Regex("Address.Street", new MongoDB.Bson.BsonRegularExpression(options.Address, "i")),
                filterBuilder.Regex("Address.City", new MongoDB.Bson.BsonRegularExpression(options.Address, "i"))
            );

        if (options.MinPrice.HasValue)
            filter &= filterBuilder.Gte("Price.Amount", options.MinPrice.Value);

        if (options.MaxPrice.HasValue)
            filter &= filterBuilder.Lte("Price.Amount", options.MaxPrice.Value);

        return filter;
    }

    public async Task<IEnumerable<Property>> GetByFilterAsync(PropertyFilterOptions options) {
        var filter = BuildFilter(options);

        var docs = await _collection
            .Find(filter)
            .Skip(options.PageSize * (options.Page - 1))
            .Limit(options.PageSize)
            .ToListAsync();

        return docs.Select(d => new Property(
            d.Name,
            new Address(d.Address.Street, d.Address.City, d.Address.Country),
            new Price(d.Price.Amount),
            d.OwnerId,
            d.CodeInternal,
            d.Year,
            d.PropertyImage.Select(pi => new PropertyImage(pi.ImageUrl, pi.Enabled)).ToList(),
            d.PropertyTrace.Select(pt => new PropertyTrace(pt.Date, pt.Name, pt.Value, pt.Tax)).ToList(),
            new PresentationConfig(d.PresentationConfig.CoverImageIndex, d.PresentationConfig.ListClass)
        ));
    }
}
