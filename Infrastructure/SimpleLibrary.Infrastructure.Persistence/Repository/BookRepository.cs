using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SimpleLibrary.Core;
using SimpleLibrary.Domain.Book;

namespace SimpleLibrary.Persistence.Repository
{
    public class BookRepository : BaseRepository
    {
        private readonly DbContext _context;

        public BookRepository(DbContext context) : base(context)
        {
            _context = context;
        }


        public IObservable<IList<Book>> GetBooksByNamesAsync(string names) =>
            ObservableFactory.Create(() => GetBooksByNames(names));

        private IList<Book> GetBooksByNames(string names)
        {
            return Set<Book>().FromSqlRaw("call sp_getBooksByNames({0})",names).ToList();
        }

        public Book? getBook(string name)
        {
            return Set<Book>().FirstOrDefault(q => q.Name == name);
        }

        public int AddBook(Book book)
        {
            Set<Book>().Add(book);

            return _context.SaveChanges();
        }
    }
}