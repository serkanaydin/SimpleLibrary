using System.Threading.Tasks;
using SimpleLibrary.Core.Dtos;
using SimpleLibrary.Persistence.Repository;

namespace SimpleLibrary.Application
{
    public class BookTypeService
    {
        private BookTypeRepository _bookTypeRepository;
        public BookTypeService(BookTypeRepository bookTypeRepository)
        {
            _bookTypeRepository = bookTypeRepository;
        }
        public async Task<SearchByBookTypeResultDto> GetBookTypeWithBooks(string type,int currentPage){
            return await _bookTypeRepository.getBookTypeWithBooks(type,currentPage);
        }
    }
}