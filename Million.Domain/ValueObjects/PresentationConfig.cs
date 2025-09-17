namespace Million.Domain.ValueObjects;

public class PresentationConfig
{
    public int CoverImageIndex { get; }
    public string ListClass { get; }

    public PresentationConfig(int coverImageIndex, string listClass) {
        if (coverImageIndex < 0) throw new ArgumentException("Cover image index cannot be negative");
        if (string.IsNullOrWhiteSpace(listClass)) throw new ArgumentException("ListClass is required");

        CoverImageIndex = coverImageIndex;
        ListClass = listClass;
    }
}