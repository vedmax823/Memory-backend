using Memory.DTOs;
using Memory.DTOs.cardDtos;
using Memory.Services.CardService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Memory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly ICardService _service;

        public CardController(ICardService service)
        {
            _service = service;
        }

        [HttpPatch]
        [Authorize]
        public async Task<IActionResult> UpdateCard(UpdateCardDto updateCardDto)
        {
            if (!Guid.TryParse(User.FindFirst("id")?.Value, out Guid userId))
            {
                return Unauthorized();
            }
            try
            {
                var card = await _service.UpdateCard(updateCardDto, userId);
                return Ok(card);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An unexpected error occurred." });
            }
        }
    }

}

