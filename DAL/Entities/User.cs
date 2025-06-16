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
    
    public IEnumerable<SavedPost>? SavedPosts { get; set; } = new List<SavedPost>();
    public IEnumerable<Publication>? Publications { get; set; } = new List<Publication>();
    public IEnumerable<PostLike>? PostLikes { get; set; } = new List<PostLike>();
    public IEnumerable<CommentLike>? CommentLikes { get; set; } = new List<CommentLike>();
    public IEnumerable<Comment>? Comments { get; set; } = new List<Comment>();
    
    public IEnumerable<Follower>? Followers { get; set; } = new List<Follower>();
    public IEnumerable<Follower>? Following { get; set; } = new List<Follower>();
}