namespace DAL.Entities;

public class Outfit
{
    public int Id { get; set; }
    
    public int UserID { get; set; }

    public int TemperatureSuitabilityID { get; set; }
    
    public User? User { get; set; }
    public TemperatureSuitability? TemperatureSuitability { get; set; }
    
    public ICollection<Publication> Publications { get; set; } = new List<Publication>();
    public ICollection<OutfitTag> Tags { get; set; } = new List<OutfitTag>();
    public ICollection<OutfitStyle> Styles { get; set; } = new List<OutfitStyle>();
    public ICollection<OutfitSeason> Seasons { get; set; } = new List<OutfitSeason>();
    public ICollection<OutfitItem> Items { get; set; } = new List<OutfitItem>();
    public ICollection<OutfitGroupItem> GroupItems { get; set; } = new List<OutfitGroupItem>();
    public ICollection<Event> Events { get; set; } = new List<Event>();
}