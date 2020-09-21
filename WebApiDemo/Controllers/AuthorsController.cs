using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WebApiDemo.Data;
using WebApiDemo.Dtos;
using WebApiDemo.Models;

namespace WebApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        #region declarations
        private readonly IAuthorAPIRepository _authorAPIRepository;
        private readonly IMapper _mapper;
        #endregion

        #region constructors
        public AuthorsController(IAuthorAPIRepository authorAPIRepository, IMapper mapper)
        {
            _authorAPIRepository = authorAPIRepository;
            _mapper = mapper;
        }
        #endregion

        #region synchronous code
        //GET api/authors
        [HttpGet]
        public ActionResult<IEnumerable<AuthorReadDto>> GetAllAuthors()
        {
            var authors = _authorAPIRepository.GetAuthors();

            if (authors != null)
            {
                return Ok(_mapper.Map<IEnumerable<AuthorReadDto>>(authors));
            }

            return NotFound();
        }

        //GET api/commands/{id}
        [HttpGet("{id}", Name = "GetAuthor")]
        public ActionResult<AuthorReadDto> GetAuthor(Guid id)
        {
            var author = _authorAPIRepository.GetAuthor(id);

            if (author != null)
            {
                return Ok(_mapper.Map<AuthorReadDto>(author));
            }

            return NotFound();
        }

        //CREATE api/author/{id}
        [HttpPost]
        public ActionResult<AuthorReadDto> CreateAuthor(AuthorCreateDto authorCreateDto)
        {
            var author = _mapper.Map<Author>(authorCreateDto);

            _authorAPIRepository.CreateAuthor(author);

            if (_authorAPIRepository.SaveChanges())
            {
                var authorReadDto = _mapper.Map<AuthorReadDto>(author);

                return CreatedAtAction(nameof(GetAuthor), new { author.Id }, authorReadDto);
            }

            return NoContent();
        }

        //PUT api/author
        [HttpPut("{id}")]
        public ActionResult UpdateAuthor(Guid id, AuthorUpdateDto authorUpdateDto)
        {
            var author = _authorAPIRepository.GetAuthor(id);

            if (author == null)
            {
                return NotFound();
            }

            _mapper.Map(authorUpdateDto, author);

            _authorAPIRepository.UpdateAuthor(author);

            _authorAPIRepository.SaveChanges();

            return NoContent();
        }

        //PATCH api/author
        [HttpPatch("{id}")]
        public ActionResult PartialAuthorUpdate(Guid id, JsonPatchDocument<AuthorUpdateDto> jsonPatchDocument)
        {
            var author = _authorAPIRepository.GetAuthor(id);

            if (author == null)
            {
                return NotFound();
            }

            var authorUpdate = _mapper.Map<AuthorUpdateDto>(author);
            jsonPatchDocument.ApplyTo(authorUpdate, ModelState);
            if (!TryValidateModel(authorUpdate))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(authorUpdate, author);

            _authorAPIRepository.UpdateAuthor(author);

            _authorAPIRepository.SaveChanges();

            return NoContent();
        }

        //DELETE api/author/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteBook(Guid id)
        {
            var author = _authorAPIRepository.GetAuthor(id);

            if(author == null)
            {
                return NotFound();
            }

            _authorAPIRepository.DeleteAuthor(author);

            _authorAPIRepository.SaveChanges();

            return NoContent();
        }
        #endregion

        #region asynchronous code
        //GET api/authors/getauthorsasync
        [HttpGet("GetAuthorsAsync")]
        public async Task<ActionResult<IEnumerable<AuthorReadDto>>>GetAuthorsAsync()
        {
            var authors = await _authorAPIRepository.GetAuthorsAsync();

            return authors != null ? Ok(_mapper.Map<IEnumerable<AuthorReadDto>>(authors)) : (ActionResult<IEnumerable<AuthorReadDto>>)NoContent();
        }

        //GET api/authors/getauthorasync/id
        [HttpGet("GetAuthorAsync/{id}")]
        [ActionName(nameof(GetAuthorAsync))]         //solve no route matches the supplied values 
        public async Task<ActionResult<AuthorReadDto>> GetAuthorAsync(Guid id)
        {
            var author = await _authorAPIRepository.GetAuthorAsync(id);

            return author != null ? Ok(_mapper.Map<AuthorReadDto>(author)) : (ActionResult<AuthorReadDto>)NotFound();
        }

        //CREATE api/authors/createauthorsasync
        [HttpPost("CreateAuthorAsync")]
        public async Task<ActionResult<AuthorReadDto>> CreateAuthorAsync(AuthorCreateDto authorCreateDto)
        {
            var author = _mapper.Map<Author>(authorCreateDto);

            _authorAPIRepository.CreateAuthor(author);

            return await _authorAPIRepository.SaveChangesAsync() ? CreatedAtAction(nameof(GetAuthorAsync), new { author.Id }, _mapper.Map<AuthorReadDto>(author)) : (ActionResult<AuthorReadDto>)NoContent();
        }

        //PUT api/authors/updateauthorasync/async
        [HttpPut("UpdateAuthorAsync")]
        public async Task<ActionResult> UpdateAuthorAsync(Guid id, AuthorUpdateDto authorUpdateDto)
        {
            var author = await _authorAPIRepository.GetAuthorAsync(id);
            if(author == null)
            {
                return NotFound();
            }

            _mapper.Map(authorUpdateDto, author);

            await _authorAPIRepository.UpdateAuthorAsync(author);

            await _authorAPIRepository.SaveChangesAsync();

            return NoContent();
        }

        //PATCH api/authors/partialupdateauthorasync
        [HttpPatch("PartialUpdateAuthorAsync")]
        public async Task<ActionResult> ParialUpdateAuthorAsync(Guid id, JsonPatchDocument<AuthorUpdateDto> jsonPatchDocument)
        {
            var author = await _authorAPIRepository.GetAuthorAsync(id);
            if(author == null)
            {
                return NotFound();
            }

            var authorToPatch = _mapper.Map<AuthorUpdateDto>(author);
            jsonPatchDocument.ApplyTo(authorToPatch, ModelState);
            if (!TryValidateModel(authorToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(authorToPatch, author);

            await _authorAPIRepository.UpdateAuthorAsync(author);

            await _authorAPIRepository.SaveChangesAsync();

            return NoContent();
        }

        //DELETE api/authors/deleteauthorsasync
        [HttpDelete("DeleteAuthorAsync")]
        public async Task<ActionResult> DeleteAuthorAsync(Guid id)
        {
            var author = await _authorAPIRepository.GetAuthorAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            _authorAPIRepository.DeleteAuthor(author);

            await _authorAPIRepository.SaveChangesAsync();

            return NoContent();
        }
        #endregion
    }
}
