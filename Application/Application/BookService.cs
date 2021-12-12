#nullable enable
using System;
using System.Collections.Generic;
using SimpleLibrary.Domain.Book;
using SimpleLibrary.Persistence.Repository;

namespace SimpleLibrary.Application
{
    public class BookService
    {
        private BookRepository _bookRepository;
        public BookService(BookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public Book? GetBook(string name)
        {
            return _bookRepository.getBook(name);
        }

        public IObservable<IList<Book>> GetBooksByNamesAsync(string names)
        {
            return _bookRepository.GetBooksByNamesAsync(names);
        }

        public int AddBook(Book book)
        {
            return _bookRepository.AddBook(book);
        }
    }
}