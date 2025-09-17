namespace Million.Application.DTOs;

public class PropertyTraceDto {
    public string Name { get; set; } = default!;
    public DateTime Date { get; set; } = default!;
    public decimal Value { get; set; }
    public decimal Tax { get; set; }
}