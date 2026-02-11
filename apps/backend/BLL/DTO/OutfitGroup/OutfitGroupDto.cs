namespace BLL.DTO.OutfitGroup;

public class OutfitGroupDto
{
    public int Id { get; set; }

    public string GroupName { get; set; }

    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; }
}