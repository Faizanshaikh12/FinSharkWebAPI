using Microsoft.AspNetCore.Identity;

namespace FinSharkWebAPI.Models;
public class AppUser : IdentityUser
{
    public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>();
}