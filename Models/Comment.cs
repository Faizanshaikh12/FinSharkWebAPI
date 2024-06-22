using System.ComponentModel.DataAnnotations.Schema;

namespace FinSharkWebAPI.Models;
[Table("Comments")]
public class Comment
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime Created { get; set; } = DateTime.UtcNow;
    public int? StockId { get; set; }
    public Stock? Stocks { get; set; }
}