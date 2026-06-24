using System.Text.Json.Serialization;
namespace BLL.DTO.ML;

public class ConfidenceDetails
{
    [JsonPropertyName("main")]
    public double Main { get; set; }
    
    [JsonPropertyName("sub")]
    public double Sub { get; set; }
}