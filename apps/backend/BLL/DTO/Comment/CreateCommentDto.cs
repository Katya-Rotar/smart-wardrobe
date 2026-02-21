namespace BLL.DTO.Comment;

public class CreateCommentDto
{
    public int UserID { get; set; }

    public int PublicationID { get; set; }

    public string Content { get; set; }
}