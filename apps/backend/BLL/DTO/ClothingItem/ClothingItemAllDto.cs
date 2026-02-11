namespace BLL.DTO;

public class ClothingItemAllDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Color { get; set; }
    public string? ImageURL { get; set; }
    public DateTime? LastWornDate { get; set; }
    
    public string? CategoryName { get; set; }
    public string? TypeName { get; set; }
    public string? TemperatureSuitabilityName { get; set; }

    public List<string> StyleNames { get; set; } = new();
    public List<string> SeasonNames { get; set; } = new();
}