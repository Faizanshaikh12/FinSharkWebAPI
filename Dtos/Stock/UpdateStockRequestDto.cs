using System.ComponentModel.DataAnnotations;

namespace FinSharkWebAPI.Dtos.Stock;

public class UpdateStockRequestDto
{
    [Required]
    [MaxLength(10, ErrorMessage = "Symbol cannot be over 10 characters")]
    public string Symbol { get; set; } = string.Empty;

    [Required]
    [MaxLength(10, ErrorMessage = "Company Name cannot be over 10 characters")]
    public string CompanyName { get; set; } = string.Empty;

    [Required] [Range(1, 100000000)] public decimal Purchase { get; set; }

    [Required] [Range(0.001, 100)] public decimal LasDiv { get; set; }

    [Required]
    [MaxLength(10, ErrorMessage = "Industry cannot be over 10 characters")]
    public string Industry { get; set; } = string.Empty;

    [Range(1, 500000000)] public long MarketCap { get; set; }
}