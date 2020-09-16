using AutoMapper;
using WebApiDemo.Data;
using WebApiDemo.Dtos;
using WebApiDemo.Models;

namespace WebApiDemo.AutoMapper
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<Author, AuthorReadDto>();
            CreateMap<Book, BookReadDto>();
            //CreateMap<BookCreateDto, Book>().ForSourceMember(i => i.AuthorId, o => new Guid());
            CreateMap<BookCreateDto, BookReadDto>();
            CreateMap<BookCreateDto, Book>();
            CreateMap<AuthorCreateDto, Author>();
            CreateMap<Author, AuthorReadDto>();
            CreateMap<BookUpdateDto, Book>();
            CreateMap<Book, BookUpdateDto>();
            CreateMap<Author, AuthorUpdateDto>();
            CreateMap<AuthorUpdateDto, Author>();
        }
    }
}
