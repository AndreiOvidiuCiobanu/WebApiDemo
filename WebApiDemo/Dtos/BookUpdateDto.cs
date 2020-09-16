using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiDemo.Data
{
    public class BookUpdateDto
    {
        public Guid AuthorId { get; set; }
    }
}
