using BLL.DTO.Outfit;

namespace BLL.DTO.Publication;

public class PublicationDetailsDto
{
    public int Id { get; set; }
    public string ImageURL { get; set; }
    public bool CommentingOptions { get; set; }

    public string Username { get; set; }
    public string? UserImage { get; set; }

    public List<string> Tags { get; set; } = new();
    public List<OutfitItemsDto> OutfitItemImages { get; set; } = new();
}