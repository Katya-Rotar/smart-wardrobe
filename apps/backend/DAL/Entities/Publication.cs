namespace DAL.Entities;

public class Publication
{
    public int Id { get; set; }
    
    public int ProfileID { get; set; }

    public int OutfitID { get; set; }

    public string ImageURL { get; set; }

    public bool CommentingOptions { get; set; }
    
    public Profile? Profile { get; set; }
    public Outfit? Outfit { get; set; }
    
    public IEnumerable<SavedPost>? SavedPosts { get; set; } = new List<SavedPost>();
    public IEnumerable<PublicationTag>? PublicationTags { get; set; } = new List<PublicationTag>();
    public IEnumerable<PostLike>? PostLikes { get; set; } = new List<PostLike>();
    public IEnumerable<Comment>? Comments { get; set; } = new List<Comment>();
}