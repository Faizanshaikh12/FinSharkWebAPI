using FinSharkWebAPI.Data;
using FinSharkWebAPI.Dtos.Stock;
using FinSharkWebAPI.Helpers;
using FinSharkWebAPI.Interfaces;
using FinSharkWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FinSharkWebAPI.Repository;

public class StockRepository : IStockRepository
{
    public readonly AppDbContext _context;

    public StockRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Stock>> GetAllAsync(QueryObject qeury)
    {
        var stocks = _context.Stocks.Include(c => c.Comments).AsQueryable();

        if (!string.IsNullOrWhiteSpace(qeury.Symbol)) stocks = stocks.Where(s => s.Symbol.Contains(qeury.Symbol));

        if (!string.IsNullOrWhiteSpace(qeury.CompanyName))
            stocks = stocks.Where(s => s.CompanyName.Contains(qeury.CompanyName));

        if (!string.IsNullOrWhiteSpace(qeury.CompanyName))
            stocks = stocks.Where(s => s.CompanyName.Contains(qeury.CompanyName));

        if (!string.IsNullOrWhiteSpace(qeury.SortBy))
            if (qeury.SortBy.Equals("Symbol", StringComparison.OrdinalIgnoreCase))
                stocks = qeury.IsDecsending ? stocks.OrderByDescending(s => s.Symbol) : stocks.OrderBy(s => s.Symbol);

        var skipNumber = (qeury.Page - 1) * qeury.PageSize;

        return await stocks.Skip(skipNumber).Take(qeury.PageSize).ToListAsync();
    }

    public async Task<Stock?> GetByIdAsync(int id)
    {
        return await _context.Stocks.Include(c => c.Comments).FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<Stock> CreateAsync(Stock stockModel)
    {
        await _context.Stocks.AddAsync(stockModel);
        await _context.SaveChangesAsync();
        return stockModel;
    }

    public async Task<Stock?> UpdateAsync(int id, UpdateStockRequestDto updateDto)
    {
        var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
        if (stockModel == null) return null;

        stockModel.Symbol = updateDto.Symbol;
        stockModel.CompanyName = updateDto.CompanyName;
        stockModel.Purchase = updateDto.Purchase;
        stockModel.LasDiv = updateDto.LasDiv;
        stockModel.Industry = updateDto.Industry;
        stockModel.MarketCap = updateDto.MarketCap;

        await _context.SaveChangesAsync();
        return stockModel;
    }

    public async Task<Stock?> DeleteAsync(int id)
    {
        var stockModel = await _context.Stocks.FirstOrDefaultAsync(x => x.Id == id);
        if (stockModel == null) return null;

        _context.Stocks.Remove(stockModel);
        await _context.SaveChangesAsync();
        return stockModel;
    }

    public async Task<bool> StockExists(int id)
    {
        return await _context.Stocks.AnyAsync(x => x.Id == id);
    }
}