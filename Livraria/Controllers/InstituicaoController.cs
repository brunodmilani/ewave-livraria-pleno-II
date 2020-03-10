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
    public class InstituicaoController : ControllerBase
    {
        private readonly IInstituicaoRepository _instituicaoRepository;
        public InstituicaoController(IInstituicaoRepository instituicaoRepository)
        {
            _instituicaoRepository = instituicaoRepository;
        }

        // GET: api/Editora
        [HttpGet]
        public IEnumerable<Instituicao> GetInstituicoes()
        {
            return _instituicaoRepository.GetAll();
        }

        // GET: api/Editora/5
        [HttpGet("{id}")]
        public IActionResult GetInstituicoes([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var instituicoes = _instituicaoRepository.Find(id);

            if (instituicoes == null)
            {
                return NotFound();
            }

            return Ok(instituicoes);
        }

        // PUT: api/Editora/5
        [HttpPut("{id}")]
        public IActionResult PutInstituicoes([FromRoute] long id, [FromBody] Instituicao instituicao)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != instituicao.Id)
            {
                return BadRequest();
            }

            var _instituicao = _instituicaoRepository.Find(id);
            if (_instituicao == null)
            {
                return NotFound();
            }

            try
            {
                _instituicaoRepository.Update(instituicao);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_instituicaoRepository.Exists(id))
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

        // POST: api/Editora
        [HttpPost]
        public IActionResult PostInstituicoes([FromBody] Instituicao instituicao)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _instituicaoRepository.Add(instituicao);

            return CreatedAtAction("GetInstituicoes", new { id = instituicao.Id }, instituicao);
        }

        // DELETE: api/Editora/5
        [HttpDelete("{id}")]
        public IActionResult DeleteInstituicoes([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var instituicao = _instituicaoRepository.Find(id);
            if (instituicao == null)
            {
                return NotFound();
            }

            _instituicaoRepository.Remove(instituicao.Id);

            return Ok(instituicao);
        }
    }
}