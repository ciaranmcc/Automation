using System.Text.Json.Serialization;

public record Root(
        [property: JsonPropertyName("date")] string date,
        [property: JsonPropertyName("temperatureC")] int temperatureC,
        [property: JsonPropertyName("temperatureF")] int temperatureF,
        [property: JsonPropertyName("summary")] string summary
    );