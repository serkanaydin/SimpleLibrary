using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using SimpleLibrary.Core.Dtos;
using SimpleLibrary.Core.Enum;
using SimpleLibrary.Domain;

namespace SimpleLibrary.Persistence.Repository
{
    public class BookTypeRepository : BaseRepository,IRepository 
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
        
        public async Task<BookTypeEnums> DeleteBookType(int typeId)
        {
            var dbSetBookType = Set<BookType>();
            
            var bookType=await dbSetBookType.AsNoTracking().FirstOrDefaultAsync(q => q.Id==typeId);
            if (bookType is null)
                return BookTypeEnums.BookTypeDoesntExists;

            var dbSetBook = Set<Book>();
            var books = dbSetBook.AsNoTracking().Where(q => q.BookTypeId == bookType.Id);
            
            dbSetBook.RemoveRange(books);
            dbSetBookType.Remove(bookType);
            
            var saveChangesResult = await SaveChangesAsync();
            if(saveChangesResult is 0)
                return BookTypeEnums.SaveChangesFault;
            
            return BookTypeEnums.DeletionSuccessful;
        }
        
        public async Task<BookTypeEnums> CreateBookType(BookTypeModelDto model)
        {
            var typeSet = Set<BookType>();

            var type = await typeSet.AsNoTracking().Where(q => q.Type.Equals(model.Type)).FirstOrDefaultAsync();
            if (type is not null)
                return BookTypeEnums.BookTypeAlreadyExists;
            
            var bookType = new BookType
            {
                Type = model.Type
            };
            await Set<BookType>().AddAsync(bookType);
            
            var saveChangesResult = await SaveChangesAsync();
            if(saveChangesResult is 0)
                return BookTypeEnums.SaveChangesFault;

            return BookTypeEnums.CreationSuccessful;
        }
        public async Task<SearchByBookTypeResultDto> GetBookTypeWithBooks(string type,int currentPage)
        {
            var cacheKey = $"search-books-by-booktype-{type}-{currentPage}";
            if (_cache.TryGetValue(cacheKey, out SearchByBookTypeResultDto result)) return result;
            
            var bookType =await Set<BookType>().AsNoTracking().FirstOrDefaultAsync(q => q.Type.Contains(type));
            if (bookType is null)
                return null;
            
            var bookList =await  mapper.ProjectTo<BookInfoDto>(Set<Book>().AsNoTracking().Where(q => q.BookTypeId == bookType.Id)
                .OrderBy(q => q.Name).Skip((currentPage - 1) * 20).Take(20)).ToListAsync();
            result = new SearchByBookTypeResultDto() {Type = type,CurrentPage = currentPage,TotalCount = bookList.Count, BookInfoList = bookList};
            
            _cache.Set(cacheKey, result, _cacheExpirationOptions);
            return result;
        }
    }
}