using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiDemo.Models
{
    public class Author
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        
        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; }
        
        [Required]
        [MaxLength(30)]
        public string LastName { get; set; }
        
        [Required]
        public int Age { get; set; }
        
        public ICollection<Book> Books { get; set; }
    }
}
