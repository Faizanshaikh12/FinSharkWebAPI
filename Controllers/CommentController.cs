using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinSharkWebAPI.Data;
using FinSharkWebAPI.Dtos.Stock;
using FinSharkWebAPI.Dtos.Comment;
using FinSharkWebAPI.Mappers;
using FinSharkWebAPI.Interfaces;
using Microsoft.Extensions.Logging;

namespace FinSharkWebAPI.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        public readonly AppDbContext _context;
        public readonly ICommentRepository _commentRepo;
        public readonly IStockRepository _stockRepo;
        private readonly ILogger<CommentController> _logger;

        public CommentController(AppDbContext context, ICommentRepository commentRepo, IStockRepository stockRepo, ILogger<CommentController> logger)
        {
            _commentRepo = commentRepo;
            _stockRepo = stockRepo;
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            var comment = await _commentRepo.GetAllAsync();
            var commentDto = comment.Select(s => s.ToCommentDto());
            return Ok(commentDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var comment = await _commentRepo.GetByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment.ToCommentDto());
        }

        [HttpPost]
        [Route("{stockId}")]
        public async Task<IActionResult> Create([FromRoute] int stockId, [FromBody] CreateCommentDto commentDto)
        {
         // Log the content of commentDto
            _logger.LogInformation("Received Stock ID", stockId);
            _logger.LogInformation("Received commentDto: {@CommentDto}", commentDto);

             if (!await _stockRepo.StockExists(stockId))
            {
                return BadRequest("Stock does not exist");
            }
            var commentModel = commentDto.ToCommentFromCreateDto(stockId);
            await _commentRepo.CreateAsync(commentModel);
            return CreatedAtAction(nameof(GetById), new { id = commentModel.Id }, commentModel.ToCommentDto());
        }

        // [HttpPut]
        // [Route("{id}")]
        // public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto updateDto)
        // {
        //     var stockModel = await _stockRepo.UpdateAsync(id, updateDto);
        //     if (stockModel == null)
        //     {
        //         return NotFound();
        //     }

        //     return Ok(stockModel.ToStockDto());
        // }

        // [HttpDelete]
        // [Route("{id}")]
        // public async Task<IActionResult> Delete([FromRoute] int id)
        // {
        //     var stockModel = await _stockRepo.DeleteAsync(id);
        //     if (stockModel == null)
        //     {
        //         return NotFound();
        //     }

        //     return NoContent();
        // }
    }
}