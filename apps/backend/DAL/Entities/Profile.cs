namespace DAL.Entities;

public class Profile
{
    public int Id { get; set; }
    
    public int UserID { get; set; }

    public string Username { get; set; }

    public string? ProfileImage { get; set; }

    public string? Bio { get; set; }
    
    public User? User { get; set; }
    
    public IEnumerable<SavedPost>? SavedPosts { get; set; } = new List<SavedPost>();
    public IEnumerable<Publication>? Publications { get; set; } = new List<Publication>();
    public IEnumerable<PostLike>? PostLikes { get; set; } = new List<PostLike>();
    public IEnumerable<CommentLike>? CommentLikes { get; set; } = new List<CommentLike>();
    public IEnumerable<Comment>? Comments { get; set; } = new List<Comment>();
    
    public IEnumerable<Follower>? Followers { get; set; } = new List<Follower>();
    public IEnumerable<Follower>? Following { get; set; } = new List<Follower>();
}