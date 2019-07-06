using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpenSpace.Models;
using OpenSpace.Request;
using OpenSpace.Sevices.Interface;
using System;
using System.Collections.Generic;

namespace OpenSpace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        public readonly IBookService bookService;

        public BooksController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        // GET api/books
        [HttpGet]
        public ActionResult<List<Book>> Get()
        {
            return bookService.GetAll();
        }

        // GET api/books/5
        [HttpGet("{id}")]
        public ActionResult<Book> Get(int id)
        {
            try
            {
                return bookService.Get(id);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // POST api/books
        [HttpPost]
        public ActionResult<int> Post(BookRequest request)
        {
            try
            {
                int newBookId = bookService.Post(request);
                return Ok(newBookId);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // PUT api/books/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, BookRequest request)
        {
            try
            {

                bookService.Put(id, request);
                return Ok("Livro editado com sucesso");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        // DELETE api/books/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {

            try
            {
                bookService.Delete(id);
                return Ok("Livro deletado com sucesso");
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
