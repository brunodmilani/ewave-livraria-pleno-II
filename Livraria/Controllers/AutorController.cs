using Livraria.Models;
using Livraria.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Livraria.Controllers
{
    [Route("api/[controller]")]
    public class AutorController : ControllerBase
    {
        private readonly IAutorRepository _autorRepository;
        public AutorController(IAutorRepository autorRepository)
        {
            _autorRepository = autorRepository;
        }

        // GET: api/Autor
        [HttpGet]
        public IEnumerable<Autor> GetAutores()
        {
            return _autorRepository.GetAll();
        }

        // GET: api/Autor/5
        [HttpGet("{id}")]
        public IActionResult GetAutores([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var autores = _autorRepository.Find(id);

            if (autores == null)
            {
                return NotFound();
            }

            return Ok(autores);
        }

        // PUT: api/Autor/5
        [HttpPut("{id}")]
        public IActionResult PutAutores([FromRoute] long id, [FromBody] Autor autor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != autor.Id)
            {
                return BadRequest();
            }

            var _autor = _autorRepository.Find(id);
            if (_autor == null)
            {
                return NotFound();
            }

            try
            {
                _autorRepository.Update(autor);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_autorRepository.Exists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Autor
        [HttpPost]
        public IActionResult PostAutores([FromBody] Autor autor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _autorRepository.Add(autor);

            return CreatedAtAction("GetAutores", new { id = autor.Id }, autor);
        }

        // DELETE: api/Autor/5
        [HttpDelete("{id}")]
        public IActionResult DeleteAutores([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var autor = _autorRepository.Find(id);
            if (autor == null)
            {
                return NotFound();
            }

            _autorRepository.Remove(id);

            return Ok(autor);
        }
    }
}