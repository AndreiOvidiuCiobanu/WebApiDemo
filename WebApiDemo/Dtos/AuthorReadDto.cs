using System.Collections.Generic;
using WebApiDemo.Models;

namespace WebApiDemo.Dtos
{
    public class AuthorReadDto : AuthorDto
    {
        public ICollection<Book> Books { get; set; }
    }
}
