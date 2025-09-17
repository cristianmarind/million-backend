using Million.Application.Interfaces;
using Million.Domain.Entities;
using Million.Domain.ValueObjects;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Million.Infrastructure.Repositories;

public class OwnerRepository : IOwnerRepository {
    private readonly IMongoCollection<OwnerDocument> _collection;

    public OwnerRepository(IMongoDatabase database) {
        _collection = database.GetCollection<OwnerDocument>("Owners");
    }

    private FilterDefinition<OwnerDocument> BuildFilter(OwnerFilterOptions options) {
        var filterBuilder = Builders<OwnerDocument>.Filter;
        var filter = filterBuilder.Empty;

        if (options.OwnerIdList != null && options.OwnerIdList.Any())
            filter &= filterBuilder.In(p => p.Id, options.OwnerIdList);

        return filter;
    }

    public async Task<IEnumerable<Owner>> GetByFilterAsync(OwnerFilterOptions options) {
        var filter = BuildFilter(options);

        var docs = await _collection
            .Find(filter)
            .ToListAsync();

        return docs.Select(d => new Owner(
            d.Name,
            d.Address,
            d.Photo,
            d.Birthday
        ));
    }
}
