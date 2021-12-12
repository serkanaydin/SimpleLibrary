using AutoMapper;
using SimpleLibrary.Core.Dtos;
using SimpleLibrary.Domain.Book;

namespace SimpleLibrary.Persistence.DtoMappings
{
    public class BookProfile :Profile
    {
        public BookProfile()
        {
            this.CreateMap<Book, BookInfoDto>()
                .ForMember(dest => dest.BookName,
                    opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.AuthorName,
                    opt => opt.MapFrom(src => src.Author.Name))
                .ForMember(dest => dest.BookType,
                    opt => opt.MapFrom(src => src.BookType.Type));
            
        }
    }
}