using FinSharkWebAPI.Dtos.Stock;
using FinSharkWebAPI.Helpers;
using FinSharkWebAPI.Models;

namespace FinSharkWebAPI.Interfaces;

public interface IStockRepository
{
    Task<List<Stock>> GetAllAsync(QueryObject query);
    Task<Stock?> GetByIdAsync(int id);
    Task<Stock> CreateAsync(Stock stockModel);
    Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto updateDto);
    Task<Stock?> DeleteAsync(int id);
    Task<bool> StockExists(int id);
}