namespace DAL.Entities;

public class Tag
{
    public int Id { get; set; }
    
    public string TagName { get; set; }
    
    public IEnumerable<PublicationTag>? PublicationTags { get; set; } = new List<PublicationTag>();
    public IEnumerable<OutfitTag>? Tags { get; set; } = new List<OutfitTag>();
}