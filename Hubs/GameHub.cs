using Microsoft.AspNetCore.SignalR;


namespace Memory.Hubs
{
    public class GameHub : Hub
    {
        public async Task JoinGame(string gameId)
        {
            try
            {
                if (string.IsNullOrEmpty(gameId))
                {
                    throw new ArgumentException("Game ID cannot be null or empty.");
                }

                await Groups.AddToGroupAsync(Context.ConnectionId, gameId);
                await Clients.Group(gameId).SendAsync("ReceiveMessage", $"User {Context.ConnectionId} приєднався до гри {gameId}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in JoinGame: {ex.Message}");
                throw; // Re-throw the exception to inform the client of the error
            }
        }
    }
}