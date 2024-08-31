using System;

namespace Memory.Entities;

public class Game
{
    public Guid Id { get; set; }
    public int MaxPlayersCount { get; set; }
    public int CardsCount {get; set;}
    public int Cols {get; set;}
    public int Rows {get; set;}
    public List<User> Users {get; set;} = [];
    public List<Card> Field { get; set; } = [];
    public bool IsStarted {get; set;} = false;
    public bool IsFinished {get; set;} = false;
    public Guid TurnUser {get; set;}
    public List<Guid> OpenCards {get; set;} = [];
}
