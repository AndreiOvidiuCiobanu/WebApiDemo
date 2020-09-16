using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApiDemo.Models;

namespace WebApiDemo.Dtos
{
    public class BookReadDto : BookDto
    {
        public Author Author { get; set; }
    }
}
