namespace SimpleLibrary.Core.Dtos
{
    public record BookInfoDto
    { 
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public string BookType { get; set; }
        public decimal Price { get; set; }
    }
}