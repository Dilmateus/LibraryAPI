using AutoMapper;
using LibraryApi.Domain.Dtos;
using LibraryApi.Domain.Entities;

namespace LibraryApi.Domain.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, User>().ReverseMap();
            CreateMap<UserDto, User>();

            CreateMap<Author, AuthorDto>().ReverseMap();
            CreateMap<AuthorDto, Author>();

            CreateMap<Book, BookDto>().ReverseMap();
            CreateMap<BookDto, Book>();

            CreateMap<BookLoan, BookLoanDto>().ReverseMap();
            CreateMap<BookLoanDto, BookLoan>();

        }
    }
}
