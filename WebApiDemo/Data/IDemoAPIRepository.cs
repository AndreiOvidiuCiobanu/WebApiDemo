using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiDemo.Models;

namespace WebApiDemo.Data
{
    public interface IBookAPIRepository
    {
        public IEnumerable<Book> GetBooks();

        public Task<IEnumerable<Book>> GetBooksAsync();

        public Book GetBook(Guid id);

        public Task<Book> GetBookAsync(Guid id);

        public void CreateBook(Book book);

        public bool SaveChanges();

        public Task<bool> SaveChangesAsync();

        public void UpdateBook(Book book);

        public Task UpdateBookAsync(Book book);

        public void DeleteBook(Book book);
    }

    public interface IAuthorAPIRepository
    {
        public IEnumerable<Author> GetAuthors();

        public Task<IEnumerable<Author>> GetAuthorsAsync();

        public Author GetAuthor(Guid id);

        public Task<Author> GetAuthorAsync(Guid id);

        public void CreateAuthor(Author author);

        public bool SaveChanges();

        public Task<bool> SaveChangesAsync();

        public void UpdateAuthor(Author author);

        public Task UpdateAuthorAsync(Author author);

        public void DeleteAuthor(Author author);
    }
}
