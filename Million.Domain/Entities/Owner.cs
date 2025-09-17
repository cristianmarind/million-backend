namespace Million.Domain.Entities;

public class Owner
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; }
    public string Address { get; private set; }
    public string Photo { get; private set; } // podría ser una URL/base64
    public DateTime Birthday { get; private set; }

    public Owner(string name, string address, string photo, DateTime birthday)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Owner name is required", nameof(name));

        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentException("Owner address is required", nameof(address));

        if (birthday == default)
            throw new ArgumentException("Owner birthday must be valid", nameof(birthday));

        Name = name;
        Address = address;
        Photo = photo;
        Birthday = birthday;
    }
}