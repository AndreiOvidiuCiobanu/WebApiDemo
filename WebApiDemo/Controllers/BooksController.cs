using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebApiDemo.Data;
using WebApiDemo.Dtos;
using WebApiDemo.Models;

namespace WebApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookAPIRepository _bookApiRepository;
        private readonly IMapper _mapper;

        public BooksController(IBookAPIRepository bookApiRepository, IMapper mapper)
        {
            _bookApiRepository = bookApiRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<BookReadDto>> GetBooks()
        {
            var books = _bookApiRepository.GetBooks();

            if (books != null)
            {
                return Ok(_mapper.Map<IEnumerable<BookReadDto>>(books));
            }

            return NoContent();
        }

        [HttpGet("{id}", Name = "GetBook")]
        public ActionResult<BookReadDto> GetBook(Guid id)
        {
            var book = _bookApiRepository.GetBook(id);

            if (book != null)
            {
                return Ok(_mapper.Map<BookReadDto>(book));
            }

            return NotFound();
        }

        //POST api/books
        [HttpPost]
        public ActionResult<BookReadDto> CreateBook(BookCreateDto bookCreateDto)
        {
            var book = _mapper.Map<Book>(bookCreateDto);

            _bookApiRepository.CreateBook(book);

            if (_bookApiRepository.SaveChanges())
            {
                var bookReadDto = _mapper.Map<BookReadDto>(bookCreateDto);
                return CreatedAtRoute(nameof(GetBook), new { book.Id }, bookReadDto);
            }

            return NoContent();
        }

        //PUT api/books/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateBook(Guid id, BookUpdateDto bookUpdateDto)
        {
            var bookFromRepo = _bookApiRepository.GetBook(id);
            if (bookFromRepo == null)
            {
                return NotFound();
            }

            _mapper.Map(bookUpdateDto, bookFromRepo);

            _bookApiRepository.UpdateBook(bookFromRepo);

            _bookApiRepository.SaveChanges();

            return NoContent();
        }

        //PATCH api/books/{id}
        [HttpPatch("{id}")]
        public ActionResult PartialBookUpdate(Guid id, JsonPatchDocument<BookUpdateDto> jsonPatchDocument)
        {
            var bookFromRepo = _bookApiRepository.GetBook(id);
            if (bookFromRepo == null)
            {
                return NotFound();
            }

            var bookToPatch = _mapper.Map<BookUpdateDto>(bookFromRepo);
            jsonPatchDocument.ApplyTo(bookToPatch, ModelState);
            if (!TryValidateModel(bookToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(bookToPatch, bookFromRepo);

            _bookApiRepository.UpdateBook(bookFromRepo);

            _bookApiRepository.SaveChanges();

            return NoContent();
        }

        //DELETE api/books/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteBook(Guid id)
        {
            var bookFromRepo = _bookApiRepository.GetBook(id);
            if (bookFromRepo == null)
            {
                return NotFound();
            }

            _bookApiRepository.DeleteBook(bookFromRepo);
            _bookApiRepository.SaveChanges();

            return NoContent();
        }
    }
}
