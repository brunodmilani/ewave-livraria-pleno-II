using Livraria.Models;
using Livraria.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Livraria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservaController : ControllerBase
    {
        private readonly IReservaRepository _reservaRepository;
        public ReservaController(IReservaRepository reservaRepository)
        {
            _reservaRepository = reservaRepository;
        }

        // GET: api/Reserva
        [HttpGet]
        public IEnumerable<Reserva> GetReservas()
        {
            return _reservaRepository.GetAll();
        }

        // GET: api/Reserva/5
        [HttpGet("{id}")]
        public IActionResult GetReserva([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reserva = _reservaRepository.Find(id);

            if (reserva == null)
            {
                return NotFound();
            }

            return Ok(reserva);
        }

        // PUT: api/Reserva/5
        [HttpPut("{id}")]
        public IActionResult PutReserva([FromRoute] long id, [FromBody] Reserva reserva)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != reserva.Id)
            {
                return BadRequest();
            }

            var _reserva = _reservaRepository.Find(id);
            if (_reserva == null)
            {
                return NotFound();
            }

            try
            {
                _reservaRepository.Update(reserva);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_reservaRepository.Exists(id))
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

        // POST: api/Reserva
        [HttpPost]
        public IActionResult PostReserva([FromBody] Reserva reserva)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _reservaRepository.Add(reserva);

            return CreatedAtAction("GetReserva", new { id = reserva.Id }, reserva);
        }

        // DELETE: api/Reserva/5
        [HttpDelete("{id}")]
        public IActionResult DeleteReserva([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reserva = _reservaRepository.Find(id);
            if (reserva == null)
            {
                return NotFound();
            }

            _reservaRepository.Remove(id);

            return Ok(reserva);
        }

        [HttpPost("{id}/emprestimo")]
        public IActionResult Emprestimo(long id)
        {
            Reserva reserva = _reservaRepository.Find(id);
            if (_reservaRepository.QuantidadeLivrosByUsuario(reserva.UsuarioId)) // Valida se usuário ja emprestou 2 livros
            {
                return BadRequest("Usuário ultrapassou o limite de 2 livros emprestados!!");
            }

            if (_reservaRepository.ExisteEmprestimo(reserva.LivroId)) // Valida se livro está disponível
            {
                return BadRequest("Desculpa, o livro ainda não está disponível!!");
            }

            if (_reservaRepository.UsuarioBloqueado(reserva.UsuarioId)) // Valida se usuário está bloqueado
            {
                return BadRequest("Usuário Bloqueado!!");
            }

            try
            {
                _reservaRepository.Emprestimo(id);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}