#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SimpleLibrary.Core;
using SimpleLibrary.Core.Dtos;
using SimpleLibrary.Core.Enum;
using SimpleLibrary.Domain;

namespace SimpleLibrary.Persistence.Repository
{
    public class BookRepository : BaseRepository
    {
        private readonly DbContext _context;
        private readonly IMapper _mapper;

        public BookRepository(IMapper mapper,DbContext context) : base(context)
        {
            _mapper = mapper;
            _context = context;
        }


        public IObservable<IList<Book>> GetBooksByNamesAsync(string names) =>
            ObservableFactory.Create(() => GetBooksByNames(names));

        public IList<Book> GetBooksByNames(string nameList)
        {
            return Set<Book>().FromSqlRaw("call sp_getBooksByNames({0});",nameList).ToList();
        }
        

        public async Task<BookInfoDto?> GetBook(string name)
        {
            return await _mapper.ProjectTo<BookInfoDto>
                (Set<Book>().Where(q => q.Name == name)).FirstOrDefaultAsync();
        }

        public async Task<BookCreationResult> AddBook(CreateBookDto model)
        {
            var book=await Set<Book>().AsNoTracking().Where(q => q.Name == model.Name).FirstOrDefaultAsync();
            if (book is not null)
                return BookCreationResult.ExistingBookWithTheName;
            
            var author=await Set<Author>().AsNoTracking().Where(q => q.Id == model.AuthorId).FirstOrDefaultAsync();
            if (author is null)
                return BookCreationResult.NoAuthor;
            
            var type=await Set<BookType>().AsNoTracking().Where(q => q.Id == model.BookTypeId).FirstOrDefaultAsync();
            if (type is null)
                return BookCreationResult.NoBookType;
            
            Book newBook = new()
            {
                Name = model.Name,
                Price = model.Price,
                AuthorId = model.AuthorId,
                BookTypeId = model.BookTypeId,
                IsActive = true,
                IsDeleted = false,
                CreationDate=DateTime.Now,
            };

            await Set<Book>().AddAsync(newBook);
            var saveResult=await _context.SaveChangesAsync();

            if (saveResult is 0)
                return BookCreationResult.SaveChangesFault;

            return BookCreationResult.Successful;
        }
    }
}