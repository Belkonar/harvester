using System.Text.Json;
using harvester_parsers;

namespace harvester_parsers_tests;

public class JsonScannerTests
{
    [Test]
    public void MostJsonStuff()
    {
        const string json = """
                            {
                              "restaurant": {
                                "id": "rest_12345",
                                "name": "The Golden Fork",
                                "cuisine": "Contemporary American",
                                "locations": [
                                  {
                                    "address": "123 Main St",
                                    "city": "Boston",
                                    "state": null,
                                    "coordinates": {
                                      "latitude": 42.3601,
                                      "longitude": -71.0589
                                    },
                                    "seating_capacity": 85
                                  }
                                ],
                                "menu_items": [
                                  {
                                    "name": "Truffle Mac & Cheese",
                                    "price": 18.99,
                                    "categories": ["pasta", "vegetarian"],
                                    "allergens": ["dairy", "gluten"],
                                    "available": true
                                  }
                                ],
                                "ratings": {
                                  "overall": 4.5,
                                  "service": 4.7,
                                  "ambiance": 4.3,
                                  "food": 4.6
                                }
                              }
                            }
                            """;

        var doc = JsonSerializer.Deserialize<JsonDocument>(json);

        Assert.That(doc, Is.Not.Null);

        var scanner = new JsonScanner(doc);
        scanner.Scan();

        Assert.That(scanner.Fields, Is.Not.Empty);

        Assert.Multiple(() =>
        {
            // The Multiple thing means these will all run even if some of them fail.
            Assert.That(scanner.Fields[""], Contains.Item("object"));
            Assert.That(scanner.Fields[".restaurant.id"], Contains.Item("string"));
            Assert.That(scanner.Fields[".restaurant.locations[].state"], Contains.Item("null"));
            Assert.That(scanner.Fields[".restaurant.locations"], Contains.Item("array"));
            Assert.That(scanner.Fields[".restaurant.locations[].coordinates.latitude"], Contains.Item("number"));
            Assert.That(scanner.Fields[".restaurant.menu_items[].available"], Contains.Item("boolean"));
        });
    }

    [Test]
    public void AbstractDataTypes()
    {
        const string json = """
                            {
                              "name": "Mixed Array Example",
                              "description": "Array with different data types",
                              "mixed_array": [
                                42,
                                "Hello World",
                                {
                                  "key": "value",
                                  "active": true
                                }
                              ]
                            }
                            """;

        var doc = JsonSerializer.Deserialize<JsonDocument>(json);

        Assert.That(doc, Is.Not.Null);

        var scanner = new JsonScanner(doc);
        scanner.Scan();

        Assert.That(scanner.Fields, Is.Not.Empty);

        Assert.Multiple(() =>
        {
            Assert.That(scanner.Fields[""], Contains.Item("object"));
            Assert.That(scanner.Fields[".mixed_array[]"], Contains.Item("object"));
            Assert.That(scanner.Fields[".mixed_array[]"], Contains.Item("string"));
            Assert.That(scanner.Fields[".mixed_array[]"], Contains.Item("number"));
            Assert.That(scanner.Fields[".mixed_array[].key"], Contains.Item("string"));
        });
    }
}
