using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using SimpleLibrary.Core.Dtos;
using SimpleLibrary.Domain;

namespace SimpleLibrary.Persistence.Repository
{
    public class BookTypeRepository : BaseRepository
    {
        private readonly IMemoryCache _cache;
        private readonly MemoryCacheEntryOptions _cacheExpirationOptions;
        private readonly IMapper mapper;
        public BookTypeRepository(IMapper mapper,DbContext context,IMemoryCache cache) : base(context)
        {
            this.mapper = mapper;
            _cache = cache;
            _cacheExpirationOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpiration = DateTime.Now.AddMinutes(1),
                Priority = CacheItemPriority.Normal
            };
        }
        
        public async Task<bool?> DeleteBookType(string type)
        {
            var dbSetBookType = Set<BookType>();
            
            var bookType=await dbSetBookType.AsNoTracking().FirstOrDefaultAsync(q => q.Type.Equals(type));
            if (bookType is null)
                return null;

            var dbSetBook = Set<Book>();
            var books = dbSetBook.AsNoTracking().Where(q => q.BookTypeId == bookType.Id);
            dbSetBook.RemoveRange(books);
            
            var removeTypeResult =dbSetBookType.Remove(bookType);
            
            return await SaveChangesAsync() is 1;
        }
        
        public async Task<bool> CreateBookType(BookTypeModelDto model)
        {
            var bookType = new BookType();
            bookType.Type = model.Type;
            await Set<BookType>().AddAsync(bookType);
            return await SaveChangesAsync() is 1;
        }
        public async Task<SearchByBookTypeResultDto> GetBookTypeWithBooks(string type,int currentPage)
        {
            var cacheKey = $"search-books-by-booktype-{type}-{currentPage}";
            if (_cache.TryGetValue(cacheKey, out SearchByBookTypeResultDto result)) return result;
            
            var bookType =await Set<BookType>().AsNoTracking().FirstOrDefaultAsync(q => q.Type.Contains(type));
            if (bookType is null)
                return null;
            
            var bookList =await  mapper.ProjectTo<BookInfoDto>(Set<Book>().AsNoTracking().Where(q => q.BookTypeId == bookType.Id)
                .OrderByDescending(q => q.Id).Skip((currentPage - 1) * 100).Take(100)).ToListAsync();
            result = new SearchByBookTypeResultDto() {totalCount = bookList.Count, bookInfoList = bookList};
            
            _cache.Set(cacheKey, result, _cacheExpirationOptions);
            return result;
        }
    }
}