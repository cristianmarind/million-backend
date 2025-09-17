namespace Million.Domain.Entities;

public class PropertyImage
{
    public string File { get; private set; }
    public bool Enabled { get; private set; } = true;

    public PropertyImage(string file, bool enabled = true)
    {
        if (string.IsNullOrWhiteSpace(file))
            throw new ArgumentException("File is required", nameof(file));

        File = file;
        Enabled = enabled;
    }
}