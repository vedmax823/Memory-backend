using System;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Memory.Hubs;
using Microsoft.AspNetCore.Authorization;
using Memory.Services.CardService;
using Memory.DTOs.cardDtos;
using Memory.Services.GameService;
using Memory.Mapping;
using Memory.DTOs;

namespace Memory.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RealTimeGameController : ControllerBase
{
    private readonly IHubContext<GameHub> _hubContext;
    private readonly ICardService _cardService;
    private readonly IGameService _gameService;


    public RealTimeGameController(IHubContext<GameHub> hubContext, ICardService cardService, IGameService gameService)
    {
        _hubContext = hubContext;
        _cardService = cardService;
        _gameService = gameService;
    }



    [HttpPost("send")]
    [Authorize]
    public async Task<IActionResult> SendMessage([FromBody] ChatMessage message)
    {

        await _hubContext.Clients.All.SendAsync("ReceiveMessage", message.User, message.Message);
        return Ok();
    }

    [HttpPatch("card")]
    [Authorize]
    public async Task<IActionResult> UpdateCard(UpdateCardDto updateCardDto)
    {
        if (!Guid.TryParse(User.FindFirst("id")?.Value, out Guid userId))
        {
            return Unauthorized();
        }
        try
        {
            var game = await _cardService.UpdateCard(updateCardDto, userId);
            await _hubContext.Clients.Group(game.Id.ToString()).SendAsync("ReceiveGame", game);
            // await _hubContext.Clients.All.SendAsync("ReceiveGame", game);
            return Ok(game);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new {message = ex.Message});
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

    [HttpPost]
    [Authorize]
    [Route("game/{gameId}")]
    public async Task<IActionResult> CheckCards(Guid gameId, CheckCardsDto checkCardsDto)
    {
        if (!Guid.TryParse(User.FindFirst("id")?.Value, out Guid userId))
        {
            return Unauthorized();
        }
        try
        {
            var game = await _gameService.CheckCards(gameId, checkCardsDto.FirstId, checkCardsDto.SecondId, userId);
            await _hubContext.Clients.Group(game.Id.ToString()).SendAsync("ReceiveGame", game);
            return Ok(game.ToGameDto());
        }
        catch (ArgumentNullException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new {message = ex.Message});
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            return StatusCode(500, new { message = "An unexpected error occurred." });
        }
    }
}

public class ChatMessage
{
    public required string User { get; set; }
    public required string Message { get; set; }
}
