using System;
using System.Collections.Generic;
using System.Linq;
using WebApiDemo.Models;

namespace WebApiDemo.Data
{
    public class BookAPIRepository : IBookAPIRepository
    {
        private readonly DemoAPIContext _context;

        public BookAPIRepository(DemoAPIContext context)
        {
            _context = context;
        }

        public IEnumerable<Book> GetBooks()
        {
            return _context.Books.ToList();
        }

        public Book GetBook(Guid id)
        {
            return _context.Books.FirstOrDefault(i => i.Id == id);
        }

        public void CreateBook(Book book)
        {
            if(book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }

            _context.Books.Add(book);
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }

        public void UpdateBook(Book book)
        {
            //Nothing
        }

        public void DeleteBook(Book book)
        {
            if(book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }

            _context.Books.Remove(book);
        }
    }
}
