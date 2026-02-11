namespace DAL.Entities;

public class ClothingItem
{
    public int Id { get; set; }
    public int UserID { get; set; }

    public string Name { get; set; }

    public int CategoryID { get; set; }

    public int TypeID { get; set; }

    public string Color { get; set; }

    public int TemperatureSuitabilityID { get; set; }

    public string? ImageURL { get; set; }

    public DateTime? LastWornDate { get; set; }
    
    public User? User { get; set; }
    public Category? Category { get; set; }
    public Type? Type { get; set; }
    public TemperatureSuitability? TemperatureSuitability { get; set; }
    
    public IEnumerable<OutfitItem> Outfits { get; set; } = new List<OutfitItem>();
    public IEnumerable<ClothingItemStyle> Styles { get; set; } = new List<ClothingItemStyle>();
    public IEnumerable<ClothingItemSeason> Seasons { get; set; } = new List<ClothingItemSeason>();
}