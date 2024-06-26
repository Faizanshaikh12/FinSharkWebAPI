using FinSharkWebAPI.Dtos.Comment;
using FinSharkWebAPI.Models;

namespace FinSharkWebAPI.Mappers;

public static class CommentMappers
{
    public static CommentDto ToCommentDto(this Comment commentModel)
    {
        return new CommentDto
        {
            Id = commentModel.Id,
            Title = commentModel.Title,
            Content = commentModel.Content,
            Created = commentModel.Created,
            StockId = commentModel.StockId
        };
    }

    public static Comment ToCommentFromCreateDto(this CreateCommentDto commentModel, int stockId)
    {
        return new Comment
        {
            Title = commentModel.Title,
            Content = commentModel.Content,
            StockId = stockId
        };
    }

    public static Comment ToCommentFromUpdateDto(this UpdateCommentDto commentModel)
    {
        return new Comment
        {
            Title = commentModel.Title,
            Content = commentModel.Content
        };
    }
}