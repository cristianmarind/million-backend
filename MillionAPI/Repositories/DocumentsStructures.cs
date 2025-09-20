using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.GeoJsonObjectModel;

namespace MillionAPI.Repositories;

public class AddressDocument {
    public required string Street { get; set; }
    public required string City { get; set; }
    public required string Country { get; set; }
}

public class PriceDocument {
    public required decimal Amount { get; set; }
    public string Currency { get; set; } = "USD";
}

public class PropertyImageDocument {
    public required string ImageUrl { get; set; }
    public bool Enabled { get; set; }
}

public class PropertyTraceDocument {
    public required string Name { get; set; }
    public required DateTime Date { get; set; }
    public required decimal Value { get; set; }
    public required decimal Tax { get; set; }
}

public class PresentationConfigDocument {
    public required string ListClass { get; set; }
    public required int CoverImageIndex { get; set; }
}

public class PropertyDocument {
    [BsonId] // marca como la PK en Mongo
    public Guid Id { get; set; }
    public required int Category { get; set; }
    [BsonElement("Location")]
    public required GeoJsonPoint<GeoJson2DCoordinates> Location { get; set; }
    public required string Name { get; set; }
    public required AddressDocument Address { get; set; }
    public required PriceDocument Price { get; set; }
    public required string CodeInternal { get; set; }
    public int Year { get; set; }
    public Guid OwnerId { get; set; }
    public required List<PropertyImageDocument> PropertyImage { get; set; }
    public List<PropertyTraceDocument> PropertyTrace { get; set; } = [];
    public required PresentationConfigDocument PresentationConfig { get; set; }
    public double Distance { get; set; }
}

public class OwnerDocument {
    [BsonId] // marca como la PK en Mongo
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required string Address { get; set; }
    public required DateTime Birthday { get; set; }
    public required string Photo { get; set; }
    public required List<Guid> Properties { get; set; }
}
