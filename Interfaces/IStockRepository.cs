using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinSharkWebAPI.Models;
using FinSharkWebAPI.Dtos.Stock;

namespace FinSharkWebAPI.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync();   
        Task<Stock?> GetByIdAsync(int id);   
        Task<Stock> CreateAsync(Stock stockModel);
        Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto updateDto);
        Task<Stock?> DeleteAsync(int id);
    }
}