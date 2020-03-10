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
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        public UsuarioController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        // GET: api/Usuario
        [HttpGet]
        public IEnumerable<Usuario> GetUsuarios()
        {
            return _usuarioRepository.GetAll();
        }

        // GET: api/Usuario/5
        [HttpGet("{id}")]
        public IActionResult GetUsuario([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var usuario = _usuarioRepository.Find(id);

            if (usuario == null)
            {
                return NotFound();
            }

            return Ok(usuario);
        }

        // PUT: api/Usuario/5
        [HttpPut("{id}")]
        public IActionResult PutUsuario([FromRoute] long id, [FromBody] Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != usuario.Id)
            {
                return BadRequest();
            }

            var _usuario = _usuarioRepository.Find(id);
            if (_usuario == null)
            {
                return NotFound();
            }

            try
            {
                _usuarioRepository.Update(usuario);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_usuarioRepository.Exists(id))
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

        // POST: api/Usuario
        [HttpPost]
        public IActionResult PostUsuario([FromBody] Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _usuarioRepository.Add(usuario);

            return CreatedAtAction("GetUsuario", new { id = usuario.Id }, usuario);
        }

        // DELETE: api/Usuario/5
        [HttpDelete("{id}")]
        public IActionResult DeleteUsuario([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var usuario = _usuarioRepository.Find(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _usuarioRepository.Remove(id);

            return Ok(usuario);
        }
    }
}