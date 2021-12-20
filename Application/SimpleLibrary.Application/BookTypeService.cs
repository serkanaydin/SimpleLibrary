using System.Threading.Tasks;
using SimpleLibrary.Abstractions;
using SimpleLibrary.Core.Dtos;
using SimpleLibrary.Core.Enum;
using SimpleLibrary.Persistence.Repository;

namespace SimpleLibrary.Application
{
    public class BookTypeService : IApplicationService
    {
        private readonly BookTypeRepository _bookTypeRepository;
        public BookTypeService(BookTypeRepository bookTypeRepository)
        {
            _bookTypeRepository = bookTypeRepository;
        }
        public async Task<SearchByBookTypeResultDto> GetBooksByBookType(string type,int currentPage){
            return await _bookTypeRepository.GetBookTypeWithBooks(type,currentPage);
        }

        public async Task<BookTypeEnums> CreateBookType(BookTypeModelDto model)
        {
            return await _bookTypeRepository.CreateBookType(model);
        }
        public async Task<BookTypeEnums> DeleteBookType(int typeId)
        {
            return await _bookTypeRepository.DeleteBookType(typeId);
        }
    }
}