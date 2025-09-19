using Million.Domain.Exceptions;

namespace Million.Domain.Entities;

public class PropertyImage
{
    public string File { get; private set; }
    public bool Enabled { get; private set; } = true;

    public PropertyImage(string file, bool enabled = true)
    {
        if (string.IsNullOrWhiteSpace(file))
            throw new PropertyNotFoundException("File is required");
        /*
            Additional validations can be added here
            1. Valid URL or file path format
            2. Supported image file extensions
        */

        File = file;
        Enabled = enabled;
    }
}