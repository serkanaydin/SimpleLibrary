#nullable enable
namespace SimpleLibrary.Domain.Book
{
    public record Book : IDentifiable<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int AuthorId { get; set; }
        public Author? Author { get; set; }
        public int BookTypeId { get; set; }
        public BookType? BookType { get; set; }
    }
}
