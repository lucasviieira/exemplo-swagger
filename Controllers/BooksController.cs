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
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
    {
        public readonly IBookService bookService;

        public BooksController(IBookService bookService)
        {
            this.bookService = bookService;
        }

        // GET api/books
        /// <summary>
        /// Lista todos os livros
        /// </summary>
        /// <returns>Lista de livros</returns>
        /// <response code="500">Erro interno do servidor</response>
        [HttpGet]
        public ActionResult<List<Book>> Get()
        {
            return bookService.GetAll();
        }

        // GET api/books/5
        /// <summary>
        /// Procura um livro por meio do id
        /// </summary>
        /// <param name="id">Id do livro</param>
        /// <returns>Livro de acordo com o id</returns>
        /// <response code="404">Livro não encontrado</response>
        /// <response code="500">Erro interno do servidor</response>
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
        /// <summary>
        /// Cria um novo livro
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST api/books
        ///     {
        ///        "name": "Livro X",
        ///        "author": "Escritor Y"
        ///     }
        /// </remarks>
        /// <param name="request">Dados do novo livro</param>
        /// <returns>Id do livro inserido</returns>
        /// <response code="400">Dados do modelo nulos ou inválidos</response>
        /// <response code="500">Erro interno do servidor</response>
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
        /// <summary>
        /// Edita um livro
        /// </summary>
        /// <param name="id">Id do livro a ser editado</param>
        /// <param name="request">Dados do livro editado</param>
        /// <response code="400">Dados do modelo nulos ou inválidos</response>
        /// <response code="404">Livro não encontrado</response>
        /// <response code="500">Erro interno do servidor</response>
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
        /// <summary>
        /// Excluir um livro
        /// </summary>
        /// <param name="id">Id do livro</param>
        /// <response code="404">Livro não encontrado</response>
        /// <response code="500">Erro interno do servidor</response>
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
