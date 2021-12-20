using System.ComponentModel.DataAnnotations;

namespace SimpleLibrary.Core.Dtos;

public record CreateBookDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public decimal Price { get; set; }
    [Required]
    public int AuthorId { get; set; }
    [Required]
    public int BookTypeId { get; set; }
}