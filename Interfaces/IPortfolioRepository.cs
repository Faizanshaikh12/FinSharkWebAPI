using FinSharkWebAPI.Models;

namespace FinSharkWebAPI.Interfaces;

public interface IPortfolioRepository
{
    Task<List<Stock>> GetUserPortfolio(AppUser user);
}