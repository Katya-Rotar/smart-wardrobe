namespace DAL.Entities;

public class Style
{
    public int Id { get; set; }
    
    public string StyleName { get; set; }
    
    public IEnumerable<OutfitStyle>? Styles { get; set; } = new List<OutfitStyle>();
    public IEnumerable<ClothingItemStyle>? ClothingItems { get; set; } = new List<ClothingItemStyle>();
}