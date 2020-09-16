using System.ComponentModel.DataAnnotations;

namespace WebApiDemo.Dtos
{
    public class BookDto
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public int YearOfAppearance { get; set; }
    }
}
