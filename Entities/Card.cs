
namespace Memory.Entities;

public class Card()
{
    public Guid Id { get; set; } 
    public int Row { get; set; } 
    public int Col { get; set; } 
    public required string Link { get; set; }
    public bool IsOpen { get; set; }
}