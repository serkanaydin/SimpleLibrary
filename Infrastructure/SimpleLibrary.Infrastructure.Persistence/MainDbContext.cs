using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SimpleLibrary.Domain.Book;

namespace SimpleLibrary.Persistence
{
    public class MainDbContext : DbContext
    {
 
        public MainDbContext(DbContextOptions<MainDbContext> options)
            : base(options)
        {
            
        }
        public DbSet<Book> Books { get; set; }

    }
}
