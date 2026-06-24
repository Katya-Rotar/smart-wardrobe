namespace BLL.DTO.ML;

public class MlItemDto
{
    public int Id { get; set; }
    public int TypeID { get; set; }
    public List<int> SeasonIds { get; set; }
    public List<int> StyleIds { get; set; }
}