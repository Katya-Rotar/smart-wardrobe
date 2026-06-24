namespace BLL.DTO;

public class CreateClothingItemDto
{
    public int UserID { get; set; }
    
    public string Name { get; set; }

    public int CategoryID { get; set; }

    public int TypeID { get; set; }

    public string Color { get; set; }

    public int TemperatureSuitabilityID { get; set; }

    public string? ImageURL { get; set; }

    public DateTime? LastWornDate { get; set; }
    
    public List<int> SeasonIds { get; set; } = new();
    public List<int> StyleIds { get; set; } = new();
}