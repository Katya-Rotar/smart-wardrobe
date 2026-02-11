namespace DAL.Entities;

public class OutfitStyle
{
    public int Id { get; set; }
    
    public int OutfitID { get; set; }

    public int StyleID { get; set; }
    
    public Outfit? Outfit { get; set; }
    public Style? Style { get; set; }
}