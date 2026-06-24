namespace BLL.DTO.Publication;

public class PythonVisualSearchResponse
{
    public string Status { get; set; } = "";
    public List<PythonVisualMatchDto> Data { get; set; } = new();
}