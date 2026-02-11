namespace BLL.DTO.Outfit;

public class UpdateOutfitDto
{
    public int Id { get; set; }
    public int TemperatureSuitabilityID { get; set; }

    public List<int> TagIDs { get; set; } = new();
    public List<int> StyleIDs { get; set; } = new();
    public List<int> SeasonIDs { get; set; } = new();
    public List<int> ClothingItemIDs { get; set; } = new();
    public List<int> OutfitGroupIDs { get; set; } = new();
}
