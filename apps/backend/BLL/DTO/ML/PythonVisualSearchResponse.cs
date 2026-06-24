namespace BLL.DTO.ML;
public class PythonVisualSearchResponse
{
    public string Status { get; set; }
    public List<PythonMlResultItem> Data { get; set; }
}