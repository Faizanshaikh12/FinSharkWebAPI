using FinSharkWebAPI.Models;

namespace FinSharkWebAPI.Interfaces;

public interface ITokenService
{
    string CreateToken(AppUser user);
}