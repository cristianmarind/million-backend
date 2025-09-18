using Million.Domain.Entities;

namespace Million.Tests.Common;

public static class TestValidData {
  public static class OwnerData {
    // Definición de la estructura de tupla reutilizable
    public readonly record struct OwnerTestData(
        string Name,
        string Address,
        string Photo,
        DateTime Birthday
    );
    public static string ValidName => "John Doe";
    public static string ValidAddress => "123 Main St, New York, USA";
    public static string ValidPhoto => "https://example.com/photo.jpg";
    public static readonly DateTime ValidBirthday = DateTime.Now.AddYears(-25);

    private static readonly Dictionary<int, OwnerTestData> OwnerDataMap = new() {
        { 0, new OwnerTestData(ValidName, ValidAddress, ValidPhoto, ValidBirthday) },
        { 1, new OwnerTestData("Bob Johnson", "789 Pine Rd, Chicago, USA", "https://example.com/bob.jpg", DateTime.Now.AddYears(-40)) },
        { 2, new OwnerTestData("Carol Williams", "101 Maple Dr, Miami, USA", "https://example.com/carol.jpg", DateTime.Now.AddYears(-28)) },
        { 3, new OwnerTestData("David Brown", "202 Birch Ln, Seattle, USA", "https://example.com/david.jpg", DateTime.Now.AddYears(-35)) },
        { 4, new OwnerTestData("Emma Davis", "303 Cedar St, Boston, USA", "https://example.com/emma.jpg", DateTime.Now.AddYears(-22)) },
        { 5, new OwnerTestData("Frank Miller", "404 Elm St, Austin, USA", "https://example.com/frank.jpg", DateTime.Now.AddYears(-45)) }
    };

    public static Owner GetValidOwner(int index = 0) {
      if (OwnerDataMap.TryGetValue(index, out var data)) {
        return new Owner(data.Name, data.Address, data.Photo, data.Birthday);
      }
      throw new ArgumentOutOfRangeException(nameof(index), "No owner data found for the given index.");
    }

    public static OwnerTestData GetOwnerDataByIndex(int index) {
      if (OwnerDataMap.TryGetValue(index, out var data)) {
        return data;
      }
      throw new ArgumentOutOfRangeException(nameof(index), "No owner data found for the given index.");
    }
  }



}