namespace DAL.Helpers.Params;

public class OutfitParams : QueryParams
{
    public int UserId { get; set; }
    public string? SearchQuery { get; set; }
    
    public string? TemperatureSuitabilityName { get; set; }
    public string? StyleName { get; set; }
    public string? SeasonName { get; set; }
    public string? GroupName { get; set; }
    
}