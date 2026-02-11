namespace DAL.Helpers.Params;

public class ClothingItemParams : QueryParams
{
    public ClothingItemParams()
    {
        OrderBy = "Name";
    }

    public int UserId { get; set; }
    public string? SearchQuery { get; set; }

    public string? CategoryName { get; set; }
    public string? TypeName { get; set; }
    public string? Color { get; set; }
    public string? TemperatureSuitabilityName { get; set; }
    public string? StyleName { get; set; }
    public string? SeasonName { get; set; }
}
