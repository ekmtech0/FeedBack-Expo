using System;
using System.ComponentModel.DataAnnotations;

namespace backend.Models;

public class Area
{
    public int Id { get; set; } 
    
    [MaxLength(100)]
    public string Name { get; set; } = null!;
    public List<Expositante> Expositantes { get; set; } = null!;
    public List<Feedback> Feedbacks { get; set; } = null!;
}
