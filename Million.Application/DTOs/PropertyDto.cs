namespace Million.Application.DTOs;

public class PropertyDto {
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Address { get; set; } = default!;
    public decimal Price { get; set; }
    public string CodeInternal { get; set; } = default!;
    public int Year { get; set; }
    public Guid OwnerId { get; set; }
    public List<string> ImageUrls { get; set; } = [];
    public List<PropertyTraceDto> PropertyTraces { get; set; } = [];
    public PresentationConfigDto PresentationConfig { get; set; } = default!;
}