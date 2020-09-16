using System;
using System.Collections.Generic;
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
        private readonly IAuthorAPIRepository _authorAPIRepository;
        private readonly IMapper _mapper;

        public AuthorsController(IAuthorAPIRepository authorAPIRepository, IMapper mapper)
        {
            _authorAPIRepository = authorAPIRepository;
            _mapper = mapper;
        }

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
    }
}
