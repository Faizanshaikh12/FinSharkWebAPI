using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinSharkWebAPI.Interfaces;
using FinSharkWebAPI.Data;
using FinSharkWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using FinSharkWebAPI.Dtos.Comment;

namespace FinSharkWebAPI.Repository
{
    public class CommentRepository: ICommentRepository
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
    }
}