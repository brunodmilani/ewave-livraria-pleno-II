using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Livraria.Models;
using Livraria.Repository;

namespace Livraria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneroController : ControllerBase
    {
        private readonly IGeneroRepository _generoRepository;
        public GeneroController(IGeneroRepository generoRepository)
        {
            _generoRepository = generoRepository;
        }

        // GET: api/Genero
        [HttpGet]
        public IEnumerable<Genero> GetGeneros()
        {
            return _generoRepository.GetAll();
        }

        // GET: api/Genero/5
        [HttpGet("{id}")]
        public IActionResult GetGeneros([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var genero = _generoRepository.Find(id);

            if (genero == null)
            {
                return NotFound();
            }

            return Ok(genero);
        }

        // PUT: api/Genero/5
        [HttpPut("{id}")]
        public IActionResult PutGeneros([FromRoute] long id, [FromBody] Genero genero)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != genero.Id)
            {
                return BadRequest();
            }

            var _genero = _generoRepository.Find(id);
            if (_genero == null)
            {
                return NotFound();
            }

            try
            {
                _generoRepository.Update(genero);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_generoRepository.Exists(id))
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

        // POST: api/Genero
        [HttpPost]
        public IActionResult PostGeneros([FromBody] Genero genero)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _generoRepository.Add(genero);

            return CreatedAtAction("GetGeneros", new { id = genero.Id }, genero);
        }

        // DELETE: api/Genero/5
        [HttpDelete("{id}")]
        public IActionResult DeleteGeneros([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var genero = _generoRepository.Find(id);
            if (genero == null)
            {
                return NotFound();
            }

            _generoRepository.Remove(genero.Id);

            return Ok(genero);
        }
    }
}