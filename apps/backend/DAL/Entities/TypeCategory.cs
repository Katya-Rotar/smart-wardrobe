namespace DAL.Entities;

public class TypeCategory
{
    public int Id { get; set; }
    
    public int CategoryID { get; set; }

    public int TypeID { get; set; }   
    
    public Category? Category { get; set; }
    public Type? Type { get; set; }
}