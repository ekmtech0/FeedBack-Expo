using System;
using System.ComponentModel.DataAnnotations;

namespace backend.Models;

public class Expositante
{
    public int Id { get; set; }

    [MaxLength(100)]
    public string Name { get; set; } = null!;
    public int AreaId { get; set; }
    public Area Area { get; set; } = null!;

}
