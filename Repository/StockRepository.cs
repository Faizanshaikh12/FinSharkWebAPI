using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FinSharkWebAPI.Interfaces;
using FinSharkWebAPI.Data;
using FinSharkWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using FinSharkWebAPI.Dtos.Stock;

namespace FinSharkWebAPI.Repository
{
    public class StockRepository: IStockRepository
    {
        public readonly AppDbContext _context;

        public StockRepository(AppDbContext context)
        {
            _context = context;
        }
        
        public async Task<List<Stock>> GetAllAsync()
        {
            return await _context.Stocks.Include(c => c.Comments).ToListAsync();
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
            if (stockModel == null)
            {
                return null;
            }

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
            if (stockModel == null)
            {
                return null;
            }

            _context.Stocks.Remove(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<bool> StockExists(int id)
        {
           return await _context.Stocks.AnyAsync(x => x.Id == id);
        }
    }
}