namespace BLL.DTO.Publication;

public class PublicationListDto
{
    public int Id { get; set; }
    public string ImageURL { get; set; }
    public List<string> Tags { get; set; } = new();
}