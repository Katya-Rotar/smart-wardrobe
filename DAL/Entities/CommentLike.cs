namespace DAL.Entities;

public class CommentLike
{
    public int Id { get; set; }
    
    public int UserID { get; set; }

    public int CommentID { get; set; }
    
    public User? User { get; set; }
    public Comment? Comment { get; set; }
}