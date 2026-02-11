namespace DAL.Entities;

public class Notification
{
    public int Id { get; set; }
    
    public int UserID { get; set; }

    public bool IsRead { get; set; }

    public DateTime CreatedAt { get; set; }

    public int? EventID { get; set; }
    
    public User? User { get; set; }
    public Event? Event { get; set; }
}