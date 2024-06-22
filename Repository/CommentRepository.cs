using FinSharkWebAPI.Data;
using FinSharkWebAPI.Interfaces;
using FinSharkWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FinSharkWebAPI.Repository;

public class CommentRepository : ICommentRepository
{
    public readonly AppDbContext _context;

    public CommentRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Comment>> GetAllAsync()
    {
        return await _context.Comments.ToListAsync();
    }

    public async Task<Comment?> GetByIdAsync(int id)
    {
        return await _context.Comments.FindAsync(id);
    }

    public async Task<Comment> CreateAsync(Comment commentModel)
    {
        await _context.Comments.AddAsync(commentModel);
        await _context.SaveChangesAsync();
        return commentModel;
    }

    public async Task<Comment?> UpdateAsync(int id, Comment updateDto)
    {
        var commentModel = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
        if (commentModel == null) return null;

        commentModel.Title = updateDto.Title;
        commentModel.Content = updateDto.Content;

        await _context.SaveChangesAsync();
        return commentModel;
    }

    public async Task<Comment?> DeleteAsync(int id)
    {
        var commentModel = await _context.Comments.FirstOrDefaultAsync(x => x.Id == id);
        if (commentModel == null) return null;

        _context.Comments.Remove(commentModel);
        await _context.SaveChangesAsync();
        return commentModel;
    }
}