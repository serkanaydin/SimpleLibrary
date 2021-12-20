#nullable enable
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SimpleLibrary.Abstractions;
using SimpleLibrary.Core.Dtos;
using SimpleLibrary.Core.Enum;
using SimpleLibrary.Domain;
using SimpleLibrary.Persistence.Repository;

namespace SimpleLibrary.Application
{
    public class BookService : IApplicationService
    {
        private BookRepository _bookRepository;
        public BookService(BookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<BookInfoDto?> GetBook(string name)
        {
            return await _bookRepository.GetBook(name);
        }

        public IObservable<IList<Book>> GetBooksByNamesAsync(string names)
        {
            return _bookRepository.GetBooksByNamesAsync(names);
        }

        public async Task<BookCreationResult> AddBook(CreateBookDto model)
        {
            return await _bookRepository.AddBook(model);
        }
    }
}