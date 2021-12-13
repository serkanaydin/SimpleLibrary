#nullable enable
using System.Collections.Generic;

namespace SimpleLibrary.Domain
{
    public class BookType :IDentifiable<int>
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public ICollection<Book>? Books { get; set; }

    }
}