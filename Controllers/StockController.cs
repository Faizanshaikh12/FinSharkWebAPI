using FinSharkWebAPI.Data;
using FinSharkWebAPI.Dtos.Stock;
using FinSharkWebAPI.Helpers;
using FinSharkWebAPI.Interfaces;
using FinSharkWebAPI.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace FinSharkWebAPI.Controllers;

[Route("api/stock")]
[ApiController]
public class StockController : ControllerBase
{
    public readonly AppDbContext _context;
    public readonly IStockRepository _stockRepo;

    public StockController(AppDbContext context, IStockRepository stockRepo)
    {
        _stockRepo = stockRepo;
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var stocks = await _stockRepo.GetAllAsync(query);
        var stocksDto = stocks.Select(s => s.ToStockDto());
        return Ok(stocksDto);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var stock = await _stockRepo.GetByIdAsync(id);
        if (stock == null) return NotFound();
        return Ok(stock.ToStockDto());
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var stockModel = stockDto.ToStockFromCreateDto();
        await _stockRepo.CreateAsync(stockModel);
        return CreatedAtAction(nameof(GetById), new { id = stockModel.Id }, stockModel.ToStockDto());
    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var stockModel = await _stockRepo.UpdateAsync(id, updateDto);
        if (stockModel == null) return NotFound();

        return Ok(stockModel.ToStockDto());
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var stockModel = await _stockRepo.DeleteAsync(id);
        if (stockModel == null) return NotFound();

        return NoContent();
    }
}