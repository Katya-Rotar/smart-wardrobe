namespace DAL.Entities;

public class PublicationTag
{
    public int Id { get; set; }
    
    public int PublicationID { get; set; }

    public int TagID { get; set; }
    
    public Publication? Publication { get; set; }
    public Tag? Tag { get; set; }
}