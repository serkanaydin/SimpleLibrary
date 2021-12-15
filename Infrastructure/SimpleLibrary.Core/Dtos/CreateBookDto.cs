namespace SimpleLibrary.Core.Dtos;

public record CreateBookDto
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int AuthorId { get; set; }
    public int BookTypeId { get; set; }
}