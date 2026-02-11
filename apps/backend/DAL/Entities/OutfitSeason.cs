namespace DAL.Entities;

public class OutfitSeason
{
    public int Id { get; set; }
    
    public int OutfitID { get; set; }

    public int SeasonID { get; set; }
    
    public Outfit? Outfit { get; set; }
    public Season? Season { get; set; }
}