namespace BLL.DTO.ML;

using System.Text.Json.Serialization;

public class MlPredictionResult
{
    [JsonPropertyName("subCategory")]
    public string MainGroup { get; set; }

    [JsonPropertyName("articleType")]
    public string SubCategory { get; set; }

    [JsonPropertyName("season")]
    public string Season { get; set; }

    [JsonPropertyName("baseColour")]
    public string Color { get; set; }

    [JsonPropertyName("usage")]
    public string Style { get; set; }

    [JsonPropertyName("confidence")]
    public ConfidenceDetails Confidence { get; set; }
}