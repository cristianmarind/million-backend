namespace Million.Application.DTOs;

public class OwnerDto {
    public string Name { get; set; } = default!;
    public string Address { get; set; } = default!;
    public string Photo { get; set; } = default!;
    public DateTime Birthday { get; set; } = default!;
}