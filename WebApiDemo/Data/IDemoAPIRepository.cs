using System;
using System.Collections.Generic;
using WebApiDemo.Models;

namespace WebApiDemo.Data
{
    public interface IBookAPIRepository
    {
        public IEnumerable<Book> GetBooks();

        public Book GetBook(Guid id);

        public void CreateBook(Book book);

        public bool SaveChanges();

        public void UpdateBook(Book book);

        public void DeleteBook(Book book);
    }

    public interface IAuthorAPIRepository
    {
        public IEnumerable<Author> GetAuthors();

        public Author GetAuthor(Guid id);

        public void CreateAuthor(Author author);

        public bool SaveChanges();

        public void UpdateAuthor(Author author);

        public void DeleteAuthor(Author author);
    }
}
