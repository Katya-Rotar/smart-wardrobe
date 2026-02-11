namespace BLL.DTO.OutfitGroup;

public class CreateOutfitGroupDto
{
    public int UserID { get; set; }

    public string GroupName { get; set; }

    public string? Description { get; set; }
}