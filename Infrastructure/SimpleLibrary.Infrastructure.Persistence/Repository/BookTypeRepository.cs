using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using SimpleLibrary.Core.Dtos;
using SimpleLibrary.Domain;
using SimpleLibrary.Domain.Book;

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
        
        public async Task<SearchByBookTypeResultDto> getBookTypeWithBooks(string type,int currentPage)
        {
            var cacheKey = $"search-books-by-booktype-{type}-{currentPage}";
            if (_cache.TryGetValue(cacheKey, out SearchByBookTypeResultDto result)) return result;
            
            var bookType = Set<BookType>().AsNoTracking().FirstOrDefault(q => q.Type.Contains(type));
            if (bookType is null)
                return null;
            
            var bookList = mapper.ProjectTo<BookInfoDto>(Set<Book>().AsNoTracking().Where(q => q.BookTypeId == bookType.Id)
                .OrderByDescending(q => q.Id).Skip((currentPage - 1) * 100).Take(100)).ToList();
            result = new SearchByBookTypeResultDto() {totalCount = bookList.Count, bookInfoList = bookList};
            
            _cache.Set(cacheKey, result, _cacheExpirationOptions);
            return result;
        }
    }
}