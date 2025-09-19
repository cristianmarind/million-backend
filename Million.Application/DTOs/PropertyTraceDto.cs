using System.ComponentModel.DataAnnotations;

namespace Million.Application.DTOs;

public class PropertyTraceDto {
    [Required]
    public string Name { get; set; } = default!;
    [Required]
    public DateTime Date { get; set; } = default!;
    [Required]
    public decimal Value { get; set; }
    [Required]
    public decimal Tax { get; set; }
}