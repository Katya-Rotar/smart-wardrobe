namespace BLL.DTO;

public class ClothingItemDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int CategoryID { get; set; }

    public int TypeID { get; set; }

    public string Color { get; set; }

    public int TemperatureSuitabilityID { get; set; }

    public string? ImageURL { get; set; }

    public DateTime? LastWornDate { get; set; }
}