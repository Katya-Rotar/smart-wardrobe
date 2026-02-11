namespace DAL.Entities;

public class CommentLike
{
    public int Id { get; set; }
    
    public int ProfileID { get; set; }

    public int CommentID { get; set; }
    
    public Profile? Profile { get; set; }
    public Comment? Comment { get; set; }
}