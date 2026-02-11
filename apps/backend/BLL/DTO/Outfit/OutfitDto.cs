namespace BLL.DTO.Outfit;

public class OutfitDto
{
    public int Id { get; set; }
    public string? TemperatureSuitabilityName { get; set; }

    public List<string> StyleNames { get; set; } = new();
    public List<string> SeasonNames { get; set; } = new();
    public List<string> Tags { get; set; } = new();
    public List<string> GroupNames { get; set; } = new();
    public List<OutfitItemsDto> ItemNames { get; set; } = new();
}