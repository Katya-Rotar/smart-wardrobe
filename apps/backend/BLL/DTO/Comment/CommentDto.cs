namespace BLL.DTO.Comment;

public class CommentDto
{
    public int Id { get; set; }
    public int UserID { get; set; }
    public string Username { get; set; }
    public string? ProfileImage { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
}