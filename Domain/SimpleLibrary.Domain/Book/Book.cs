using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleLibrary.Domain.Book
{
    public class Book 
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        
        public string Author { get; set; }
        public decimal Price { get; set; }

    }
}
