using AutoMapper;
using BookStoreApi.Models.Entities;
using BookStoreApi.Models.Repositories;

namespace BookStoreApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel, Book>();
            CreateMap<Book, BooksModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.GenreName));
            CreateMap<Book, BookModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.GenreName));
            CreateMap<Genre, GenreModel>();
            CreateMap<GenreModel, Genre>();
        }
    }
}
