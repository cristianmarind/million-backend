using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Microsoft.OpenApi.Models;
using System.Reflection;

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

builder.Services.AddControllers().AddJsonOptions(options => {
    options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
});

// Registrar Swagger generator
builder.Services.AddSwaggerGen(c =>
{
    // Esto hace que Swagger lea correctamente nullable/required
    c.SupportNonNullableReferenceTypes();
    
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mi API", Version = "v1" });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    if (File.Exists(xmlPath))
    {
        c.IncludeXmlComments(xmlPath);
    }

});

var app = builder.Build();

app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

// Middleware de manejo de errores y logger
// Sepodría tener un custom logger usando alguna herramienta de la nube o custom
// Ejm: builder.Services.AddScoped<ICustomLogger, SerilogCustomLogger>();

app.UseMiddleware<ErrorHandlingMiddleware>();

// Mostrar Swagger sólo en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // expone /swagger/v1/swagger.json
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Million API v1");
    });
}

app.MapControllers();
app.Run();