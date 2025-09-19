using System.ComponentModel.DataAnnotations;

namespace Million.Application.DTOs;

public class PresentationConfigDto {
    [Required]
    public int CoverImageIndex { get; set; } = default!;
    [Required]
    public string ListClass { get; set; } = default!;
}