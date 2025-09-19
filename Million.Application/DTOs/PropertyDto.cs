using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Million.Application.DTOs;

public class PropertyDto {
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; } = default!;
    [Required]
    public string Address { get; set; } = default!;
    [Required]
    public decimal Price { get; set; }
    [JsonIgnore]
    public string CodeInternal { get; set; } = default!;
    [Required]
    public int Year { get; set; }
    [Required]
    public Guid OwnerId { get; set; }
    [Required]
    public List<string> ImageUrls { get; set; } = [];
    [Required]
    public List<PropertyTraceDto> PropertyTraces { get; set; } = [];
    [Required]
    public PresentationConfigDto PresentationConfig { get; set; } = default!;
}