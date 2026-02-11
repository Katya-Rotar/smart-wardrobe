namespace DAL.Entities;

public class PostLike
{
    public int Id { get; set; }
    
    public int ProfileID { get; set; }

    public int PublicationID { get; set; }
    
    public Profile? Profile { get; set; }
    public Publication? Publication { get; set; }
}