namespace DAL.Entities;

public class Comment
{
    public int Id { get; set; }
    
    public int UserID { get; set; }

    public int PublicationID { get; set; }

    public string Content { get; set; }

    public DateTime CreatedAt { get; set; }
    
    public User? User { get; set; }
    public Publication? Publication { get; set; }
    
    public IEnumerable<CommentLike>? CommentLikes { get; set; } = new List<CommentLike>();
}