using System;
using System.ComponentModel.DataAnnotations;

namespace backend.Models;

public class Feedback
{
    public int Id { get; set; }
    public string BrowserId { get; set; } = null!;
    [MaxLength(100)]
    public string Name { get; set; } = null!;
    public int AreaId { get; set; }
    public Area Area { get; set; } = null!;

    [Range(1, 5)]
    public int Rating { get; set; }

    [MaxLength(500)]
    public string Comentario { get; set; } = null!;
}
