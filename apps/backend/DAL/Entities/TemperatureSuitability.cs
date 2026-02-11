using System.Collections;

namespace DAL.Entities;

public class TemperatureSuitability
{
    public int Id { get; set; }
    
    public string TemperatureSuitabilityName { get; set; }
    
    public IEnumerable<Outfit>? Outfits { get; set; } = new List<Outfit>();
    public IEnumerable<ClothingItem>? ClothingItems { get; set; } = new List<ClothingItem>();
}