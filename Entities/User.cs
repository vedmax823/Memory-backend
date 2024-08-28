using System;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace Memory.Entities;

public class User
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    [JsonIgnore]
    public List<Game>? Games { get; set; } = [];
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserNumber { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
