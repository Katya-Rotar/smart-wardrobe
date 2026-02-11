namespace DAL.Entities;

public class User 
{
    public int Id { get; set; }
    
    public string Username { get; set; }

    public string? ProfileImage { get; set; }

    public string PasswordHash { get; set; }

    public string Email { get; set; }

    public string Role { get; set; }
    
    public IEnumerable<OutfitGroup> OutfitGroups { get; set; } = new List<OutfitGroup>();
    public IEnumerable<Outfit> Outfits { get; set; } = new List<Outfit>();
    public IEnumerable<Notification> Notifications { get; set; } = new List<Notification>();
    public IEnumerable<Event> Events { get; set; } = new List<Event>();
    public IEnumerable<ClothingItem> ClothingItems { get; set; } = new List<ClothingItem>();
    public Profile Profile { get; set; }
}