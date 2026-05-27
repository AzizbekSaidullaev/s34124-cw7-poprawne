using System.ComponentModel.DataAnnotations;

namespace kolosNauka.DTOs;

public record PCRequest
{
    [Required, MaxLength(50)]
    public string Name { get; set; }
    [Required]
    public float Weight { get; set; }
    [Required]
    public int Warranty { get; set; }
    [Required]
    public DateTime CreatedAt { get; set; }
    [Required]
    public int Stock { get; set; }
}