using Livraria.Models;
using Livraria.Repository;
using Livraria.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Livraria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmprestimoController : ControllerBase
    {
        private readonly IEmprestimoRepository _emprestimoRepository;
        public EmprestimoController(IEmprestimoRepository emprestimoRepository)
        {
            _emprestimoRepository = emprestimoRepository;
        }

        // GET: api/Emprestimo
        [HttpGet]
        public IEnumerable<EmprestimoVM> GetEmprestimos()
        {
            return _emprestimoRepository.GetAll();
        }

        // GET: api/Emprestimo/5
        [HttpGet("{id}")]
        public IActionResult GetEmprestimo([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var emprestimo = _emprestimoRepository.Find(id);

            if (emprestimo == null)
            {
                return NotFound();
            }

            return Ok(emprestimo);
        }

        // PUT: api/Emprestimo/5
        [HttpPut("{id}")]
        public IActionResult PutEmprestimo([FromRoute] long id, [FromBody] Emprestimo emprestimo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != emprestimo.Id)
            {
                return BadRequest();
            }

            var _emprestimo = _emprestimoRepository.Find(id);
            if (_emprestimo == null)
            {
                return NotFound();
            }

            try
            {
                _emprestimoRepository.Update(emprestimo);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_emprestimoRepository.Exists(id))
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

        // POST: api/Emprestimo
        [HttpPost]
        public IActionResult PostEmprestimo([FromBody] Emprestimo emprestimo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_emprestimoRepository.UsuarioBloqueado(emprestimo.UsuarioId)) // Valida se usuário está bloqueado
            {
                return BadRequest("Usuário Bloqueado!!");
            }

            if (_emprestimoRepository.QuantidadeLivrosByUsuario(emprestimo.UsuarioId)) // Valida se usuário ja emprestou 2 livros
            {
                return BadRequest("Usuário ultrapassou o limite de 2 livros emprestados!!");
            }

            _emprestimoRepository.Add(emprestimo);

            return CreatedAtAction("GetEmprestimo", new { id = emprestimo.Id }, emprestimo);
        }

        // DELETE: api/Emprestimo/5
        [HttpDelete("{id}")]
        public IActionResult DeleteEmprestimo([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var emprestimo = _emprestimoRepository.Find(id);
            if (emprestimo == null)
            {
                return NotFound();
            }

            _emprestimoRepository.Remove(id);

            return Ok(emprestimo);
        }

        // PUT: api/Emprestimo/5
        [HttpPut("{id}/devolucao")]
        public IActionResult Devolucao(long id)
        {
            try
            {
                _emprestimoRepository.Devolucao(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}