using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<Author> GetAuthorAsync(Guid id)
        {
            return id != Guid.Empty ? await _context.Authors.Include(a => a.Books).FirstOrDefaultAsync(a => a.Id == id) : throw new ArgumentNullException(nameof(id));
        }

        public IEnumerable<Author> GetAuthors()
        {
            return _context.Authors.Include(a => a.Books).ToList();
        }

        public async Task<IEnumerable<Author>> GetAuthorsAsync()
        {
            return await _context.Authors.Include(a => a.Books).ToListAsync();
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }

        public void UpdateAuthor(Author author)
        {
            
        }

        public async Task UpdateAuthorAsync(Author author)
        {

        }
    }
}
