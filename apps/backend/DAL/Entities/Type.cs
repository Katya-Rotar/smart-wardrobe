using System.Collections;

namespace DAL.Entities;

public class Type
{
    public int Id { get; set; }
    
    public string TypeName { get; set; }
    
    public IEnumerable<TypeCategory> TypeCategories { get; set; } = new List<TypeCategory>();
    public IEnumerable<ClothingItem>? ClothingItems { get; set; } = new List<ClothingItem>();
}