namespace DAL.Entities;

public class Event
{
    public int Id { get; set; }
    
    public int UserID { get; set; }

    public string Name { get; set; }

    public DateTime Date { get; set; }

    public string Location { get; set; }

    public string DressCode { get; set; }

    public int? OutfitID { get; set; }
    
    public User? User { get; set; }
    public Outfit? Outfit { get; set; }
    
    public IEnumerable<Notification> Notifications { get; set; } = new List<Notification>();
}