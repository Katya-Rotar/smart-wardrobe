namespace BLL.DTO.Publication;

public class UpdatePublicationDto
{
    public int Id { get; set; }
    public int OutfitID { get; set; }
    public string ImageURL { get; set; } = string.Empty;
    public bool CommentingOptions { get; set; }
    public List<string> Tags { get; set; } = new();
}