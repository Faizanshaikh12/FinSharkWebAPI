namespace FinSharkWebAPI.Dtos.Comment;

public class CommentDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public int? StockId { get; set; }
}