using System.Collections;

namespace DAL.Entities;

public class Category
{
    public int Id { get; set; }
    
    public string CategoryName { get; set; }
    
    public IEnumerable<TypeCategory> TypeCategories { get; set; } = new List<TypeCategory>();
    public IEnumerable<ClothingItem>? ClothingItems { get; set; } = new List<ClothingItem>();
}