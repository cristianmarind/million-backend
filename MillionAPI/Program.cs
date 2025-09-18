using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

using Million.Application.Interfaces;
using Million.Application.Features.Properties.Services;
using Million.Application.Features.Owners.Services;
using MillionAPI.Repositories;


var builder = WebApplication.CreateBuilder(args);

// MongoDB

// Manage GUIDs and ObjectIds properly
BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));
builder.Services.AddSingleton<IMongoClient>(sp =>
    new MongoClient(builder.Configuration.GetConnectionString("MongoDb")));
builder.Services.AddScoped<IMongoDatabase>(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    var databaseName = builder.Configuration["DatabaseSettings:DatabaseName"];
    return client.GetDatabase(databaseName);
});

// Repositorio + servicio
builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();
builder.Services.AddScoped<IPropertyService, PropertyService>();

builder.Services.AddScoped<IOwnerRepository, OwnerRepository>();
builder.Services.AddScoped<IOwnerService, OwnerService>();

builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();
app.Run();