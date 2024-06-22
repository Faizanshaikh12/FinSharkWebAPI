using FinSharkWebAPI.Data;
using FinSharkWebAPI.Dtos.Comment;
using FinSharkWebAPI.Interfaces;
using FinSharkWebAPI.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace FinSharkWebAPI.Controllers;

[Route("api/comment")]
[ApiController]
public class CommentController : ControllerBase
{
    public readonly ICommentRepository _commentRepo;
    public readonly AppDbContext _context;
    public readonly IStockRepository _stockRepo;

    public CommentController(AppDbContext context, ICommentRepository commentRepo, IStockRepository stockRepo)
    {
        _commentRepo = commentRepo;
        _stockRepo = stockRepo;
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var comment = await _commentRepo.GetAllAsync();
        var commentDto = comment.Select(s => s.ToCommentDto());
        return Ok(commentDto);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var comment = await _commentRepo.GetByIdAsync(id);
        if (comment == null) return NotFound();
        return Ok(comment.ToCommentDto());
    }

    [HttpPost]
    [Route("{stockId:int}")]
    public async Task<IActionResult> Create([FromRoute] int stockId, [FromBody] CreateCommentDto commentDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!await _stockRepo.StockExists(stockId)) return BadRequest("Stock does not exist");
        var commentModel = commentDto.ToCommentFromCreateDto(stockId);
        await _commentRepo.CreateAsync(commentModel);
        return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDto());
    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentDto updateDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var comment = await _commentRepo.UpdateAsync(id, updateDto.ToCommentFromUpdateDto());
        if (comment == null) return NotFound("Comment not found");

        return Ok(comment.ToCommentDto());
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var comment = await _commentRepo.DeleteAsync(id);
        if (comment == null) return NotFound("Comment not found");

        return NoContent();
    }
}