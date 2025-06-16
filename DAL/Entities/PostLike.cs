namespace DAL.Entities;

public class PostLike
{
    public int Id { get; set; }
    
    public int UserID { get; set; }

    public int PublicationID { get; set; }
    
    public User? User { get; set; }
    public Publication? Publication { get; set; }
}