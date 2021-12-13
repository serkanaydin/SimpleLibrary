using System.Threading.Tasks;
using SimpleLibrary.Core.Dtos;
using SimpleLibrary.Persistence.Repository;

namespace SimpleLibrary.Application
{
    public class BookTypeService
    {
        private readonly BookTypeRepository _bookTypeRepository;
        public BookTypeService(BookTypeRepository bookTypeRepository)
        {
            _bookTypeRepository = bookTypeRepository;
        }
        public async Task<SearchByBookTypeResultDto> GetBooksByBookType(string type,int currentPage){
            return await _bookTypeRepository.GetBookTypeWithBooks(type,currentPage);
        }

        public async Task<bool> CreateBookType(BookTypeModelDto model)
        {
            return await _bookTypeRepository.CreateBookType(model);
        }
        public async Task<bool?> DeleteBookType(string type)
        {
            return await _bookTypeRepository.DeleteBookType(type);
        }
    }
}