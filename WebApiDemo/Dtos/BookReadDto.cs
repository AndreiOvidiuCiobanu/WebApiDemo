using WebApiDemo.Models;

namespace WebApiDemo.Dtos
{
    public class BookReadDto : BookDto
    {
        public Author Author { get; set; }
    }
}
