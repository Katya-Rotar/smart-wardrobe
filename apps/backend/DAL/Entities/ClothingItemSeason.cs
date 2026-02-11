namespace DAL.Entities;

public class ClothingItemSeason
{
    public int Id { get; set; }
    
    public int ClothingItemID { get; set; }

    public int SeasonID { get; set; }
    
    public ClothingItem? ClothingItem { get; set; }
    public Season? Season { get; set; }
}