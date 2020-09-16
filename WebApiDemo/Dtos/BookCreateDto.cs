using System;

namespace WebApiDemo.Dtos
{
    public class BookCreateDto : BookDto
    {
        public Guid AuthorId { get; set; }
    }
}
