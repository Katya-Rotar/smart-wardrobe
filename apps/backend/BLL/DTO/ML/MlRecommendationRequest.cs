namespace BLL.DTO.ML;

public class MlRecommendationRequest
{
    public List<MlItemDto> Items { get; set; } = new();
    public float Temperature { get; set; }
    public int WeatherCode { get; set; }
    public int? StyleId { get; set; }
    public int? SeasonId { get; set; }
}