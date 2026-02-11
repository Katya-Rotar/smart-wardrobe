namespace DAL.Entities;

public class OutfitGroup
{
    public int Id { get; set; }
    
    public int UserID { get; set; }

    public string GroupName { get; set; }

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; } 
    
    public User? User { get; set; }
    
    public IEnumerable<OutfitGroupItem>? OutfitGroups { get; set; } = new List<OutfitGroupItem>();
}