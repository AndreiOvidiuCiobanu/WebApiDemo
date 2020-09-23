using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebApiDemo.Data;
using WebApiDemo.Dtos;
using WebApiDemo.Models;
using WebApiDemo.Services;

namespace WebApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        #region declarations
        private readonly IBookAPIRepository _bookApiRepository;
        private readonly IMapper _mapper;
        private readonly IWeatherInformation _weatherInformation;
        #endregion

        #region constructors
        public BooksController(IBookAPIRepository bookApiRepository, IMapper mapper, IHttpClientFactory httpClientFactory, IWeatherInformation weatherInformation)
        {
            _bookApiRepository = bookApiRepository;
            _mapper = mapper;
            _weatherInformation = weatherInformation;
        }
        #endregion

        #region synchronous code
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

        //GET api/book
        [HttpGet("{id}", Name = "GetBook")]
        public ActionResult<BookReadDto> GetBook(Guid id)
        {
            var book = _bookApiRepository.GetBook(id);

            return book != null ? Ok(_mapper.Map<BookReadDto>(book)) : (ActionResult<BookReadDto>)NotFound();
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
        #endregion

        #region asynchronous code
        //GET api/books/getbooksasync
        [HttpGet("GetBooksAsync")]
        public async Task<ActionResult<IEnumerable<BookReadDto>>> GetBooksAsync()
        {
            var books = await _bookApiRepository.GetBooksAsync();

            return books != null ?
                Ok(_mapper.Map<IEnumerable<BookReadDto>>(books)) : (ActionResult<IEnumerable<BookReadDto>>)NoContent();
        }

        //GET api/books/getbookasync
        [HttpGet("GetBookAsync/{id}", Name = "GetBookAsync")]
        public async Task<IActionResult> GetBookAsync(Guid id)
        {
            var book = await _bookApiRepository.GetBookAsync(id);

            var weather = await _weatherInformation.GetWheatherInformation();

            return book != null ? Ok(_mapper.Map<BookReadDto>(book)) : (IActionResult)NotFound();
        }

        //POST api/books/createbookasync
        [HttpPost("CreateBookAsync")]
        public async Task<IActionResult> CreateBookAsync(BookCreateDto bookCreateDto)
        {
            var book = _mapper.Map<Book>(bookCreateDto);

            _bookApiRepository.CreateBook(book);

            return await _bookApiRepository.SaveChangesAsync() ?
                CreatedAtRoute(nameof(GetBookAsync), new { book.Id }, _mapper.Map<BookReadDto>(book)) :
                (IActionResult)NoContent();
        }

        //PUT api/books/updatebookasync
        [HttpPut("UpdateBookAsync")]
        public async Task<ActionResult> UpdateBookAsync(Guid id, BookUpdateDto bookUpdateDto)
        {
            var book = await _bookApiRepository.GetBookAsync(id);
            if(book == null)
            {
                return NotFound();
            }

            _mapper.Map(bookUpdateDto, book);

            await _bookApiRepository.UpdateBookAsync(book);

            await _bookApiRepository.SaveChangesAsync();

            return NoContent();
        }

        //PATCH api/books/partialupdatebook
        [HttpPatch("PartialUpateBook")]
        public async Task<ActionResult> PartialUpdateBook(Guid id, JsonPatchDocument<BookUpdateDto> jsonPatchDocument)
        {
            var book = await _bookApiRepository.GetBookAsync(id);
            if(book == null)
            {
                return NotFound();
            }

            var bookToPatch = _mapper.Map<BookUpdateDto>(book);
            jsonPatchDocument.ApplyTo(bookToPatch, ModelState);
            if (!TryValidateModel(bookToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(bookToPatch, book);

            await _bookApiRepository.UpdateBookAsync(book);

            await _bookApiRepository.SaveChangesAsync();

            return NoContent();
        }

        //DELETE api/books/deletebookasync
        [HttpDelete("DeleteBookAsync")]
        public async Task<ActionResult> DeleteBookAsync(Guid id)
        {
            var book = await _bookApiRepository.GetBookAsync(id);
            if(book == null)
            {
                return NotFound();
            }

            _bookApiRepository.DeleteBook(book);

            await _bookApiRepository.SaveChangesAsync();

            return NoContent();
        }
        #endregion
    }
}
