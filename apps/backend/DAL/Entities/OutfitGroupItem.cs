namespace DAL.Entities;

public class OutfitGroupItem 
{
    public int Id { get; set; }
    
    public int OutfitGroupID { get; set; }

    public int OutfitID { get; set; } 
    
    public OutfitGroup? OutfitGroup { get; set; }
    public Outfit? Outfit { get; set; }
}