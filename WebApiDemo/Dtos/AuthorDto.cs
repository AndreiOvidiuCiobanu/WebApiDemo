using System.ComponentModel.DataAnnotations;

namespace WebApiDemo.Dtos
{
    public class AuthorDto
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(30)]
        public string LastName { get; set; }

        [Required]
        [Range(10,100)]
        public int Age { get; set; }
    }
}
