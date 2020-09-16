using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApiDemo.Models;

namespace WebApiDemo.Data
{
    public class AuthorAPIRepository : IAuthorAPIRepository
    {
        private readonly DemoAPIContext _context;

        public AuthorAPIRepository(DemoAPIContext context)
        {
            _context = context;
        }

        public void CreateAuthor(Author author)
        {
            if(author == null)
            {
                throw new ArgumentNullException(nameof(author));
            }

            _context.Authors.Add(author);
        }

        public void DeleteAuthor(Author author)
        {
            _context.Authors.Remove(author);
        }

        public Author GetAuthor(Guid id)
        {
            return _context.Authors.Include(a => a.Books).FirstOrDefault(a => a.Id == id);
        }

        public IEnumerable<Author> GetAuthors()
        {
            return _context.Authors.Include(a => a.Books).ToList();
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }

        public void UpdateAuthor(Author author)
        {
            
        }
    }
}
