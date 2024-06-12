using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinSharkWebAPI.Models;
using FinSharkWebAPI.Dtos.Comment;

namespace FinSharkWebAPI.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetAllAsync();   
        Task<Comment?> GetByIdAsync(int id);   
        Task<Comment> CreateAsync(Comment commentModel);
        // Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto updateDto);
        // Task<Stock?> DeleteAsync(int id);
    }
}