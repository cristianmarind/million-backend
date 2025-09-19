using Million.Domain.Exceptions;

namespace Million.Domain.ValueObjects;

public class PresentationConfig
{
    public int CoverImageIndex { get; }
    public string ListClass { get; }

    public PresentationConfig(int coverImageIndex, string listClass) {
        if (coverImageIndex < 0) throw new PropertyContentInvalidException("Cover image index cannot be negative");
        if (string.IsNullOrWhiteSpace(listClass)) throw new PropertyNotFoundException("ListClass is required");
        /*
            Additional validations can be added here 
            1. Valid CSS class name format
        */

        CoverImageIndex = coverImageIndex;
        ListClass = listClass;
    }
}