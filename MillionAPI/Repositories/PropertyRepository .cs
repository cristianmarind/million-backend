using MongoDB.Driver;
using MongoDB.Driver.GeoJsonObjectModel;

using Million.Application.Interfaces;
using Million.Domain.Entities;
using Million.Domain.ValueObjects;

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

        if (options.category != null)
            filter &= filterBuilder.Eq("Category", options.category);

        /*if (options.latitude != null && options.longitude != null) {
            var location = new GeoJsonPoint<GeoJson2DCoordinates>(
                new GeoJson2DCoordinates(options.longitude.Value, options.latitude.Value)
            );

            filter &= filterBuilder.Near(
                x => x.Location,
                location
            );
        }*/

        return filter;
    }

    public async Task<IEnumerable<Property>> GetByFilterAsync(PropertyFilterOptions options) {
        List<PropertyDocument> docs;
        var filter = BuildFilter(options);
        var isNearSearch = options.latitude != null && options.longitude != null;

        if (!isNearSearch) {
            docs = await _collection
            .Find(filter)
            .Skip(options.PageSize * (options.Page - 1))
            .Limit(options.PageSize)
            .ToListAsync();
        }
        else {
            var coords = new double[] { options.longitude.Value, options.latitude.Value };

            docs = await _collection.Aggregate()
                .GeoNear<PropertyDocument, PropertyDocument>(
                    coords,
                    new GeoNearOptions<PropertyDocument, PropertyDocument> {
                        DistanceField = "Distance",
                        Spherical = true,
                        Query = filter
                    }
                )
                .Skip((long)(options.PageSize * (options.Page - 1)))
                .Limit((long)options.PageSize)
                .ToListAsync();
        }


        return docs.Select(d => new Property(
            d.Name,
            new Address(d.Address.Street, d.Address.City, d.Address.Country),
            new Price(d.Price.Amount),
            d.OwnerId,
            d.CodeInternal,
            d.Year,
            d.PropertyImage.Select(pi => new PropertyImage(pi.ImageUrl, pi.Enabled)).ToList(),
            d.PropertyTrace.Select(pt => new PropertyTrace(pt.Date, pt.Name, pt.Value, pt.Tax)).ToList(),
            new PresentationConfig(d.PresentationConfig.CoverImageIndex, d.PresentationConfig.ListClass),
            d.Category
        ));
    }
}
