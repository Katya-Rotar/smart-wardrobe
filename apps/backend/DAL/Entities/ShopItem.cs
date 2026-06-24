namespace DAL.Entities;

public class ShopItem
{
    public int Id { get; set; }
    public string ArticleCode { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string ProductUrl { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public double[] VggVector { get; set; } = Array.Empty<double>(); 
}