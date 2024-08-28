using System;

using Microsoft.AspNetCore.Mvc;
using Memory.DTOs;
using Microsoft.AspNetCore.Authorization;
using Memory.Services.GameService;
using Memory.Mapping;


namespace Memory.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _service;
        public GameController(IGameService service)
        {
            _service = service;
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetProducts()
        {
            return Ok(await _service.GetOpenGames());
        }

        [HttpGet]
        [Authorize]
        [Route("{id}")]
        public async Task<IActionResult> GetWith(Guid id)
        {
            var game = await _service.GetGameById(id);
            if (game is null)
                return NotFound("Game is not found");

            return Ok(game.ToGameDto());
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateGame(CreateGameDto gameDto)
        {
            if (!Guid.TryParse(User.FindFirst("id")?.Value, out Guid userId))
            {
                return Unauthorized();
            }

            try
            {
                var game = await _service.CreateGame(gameDto, userId);
                return Ok(game);
            }
            catch (ArgumentNullException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }

        }

        [HttpPost]
        [Authorize]
        [Route("{gameId}")]
        public async Task<IActionResult> CheckCards(Guid gameId, CheckCardsDto checkCardsDto)
        {
            if (!Guid.TryParse(User.FindFirst("id")?.Value, out Guid userId))
            {
                return Unauthorized();
            }
            try
            {
                var game = await _service.CheckCards(gameId, checkCardsDto.FirstId, checkCardsDto.SecondId, userId);
                return Ok(game.ToGameDto());
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, new { message = "An unexpected error occurred." });
            }
        }

        [HttpPatch]
        [Authorize]
        [Route("{gameId}")]
        public async Task<IActionResult> JoinGame(Guid gameId)
        {
            if (!Guid.TryParse(User.FindFirst("id")?.Value, out Guid userId))
            {
                return Unauthorized();
            }
            try
            {
                var game = await _service.JoinGame(gameId, userId);
                return Ok(game.ToGameDto());
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(500, new { message = "An unexpected error occurred." });
            }
        }

    }
}