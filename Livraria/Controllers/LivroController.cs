using Livraria.Models;
using Livraria.Repository;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Livraria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        private readonly ILivroRepository _livroRepository;
        public LivroController(ILivroRepository livroRepository)
        {
            _livroRepository = livroRepository;
        }

        // GET: api/Livro
        [HttpGet]
        [EnableCors("CorsPolicy")]
        public IEnumerable<Livro> GetLivros()
        {
            return _livroRepository.GetAll();
        }

        // GET: api/Livro/NotEmprestado
        [HttpGet("NotEmprestado")]
        [EnableCors("CorsPolicy")]
        public IEnumerable<Livro> GetNotEmprestado()
        {
            return _livroRepository.GetNotEmprestado();
        }

        // GET: api/Livro/5
        [HttpGet("{id}")]
        public IActionResult GetLivros([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var livro = _livroRepository.Find(id);

            if (livro == null)
            {
                return NotFound();
            }

            return Ok(livro);
        }

        // PUT: api/Livro/5
        [HttpPut("{id}")]
        public IActionResult PutLivros([FromRoute] long id, [FromBody] Livro livro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != livro.Id)
            {
                return BadRequest();
            }

            if (string.IsNullOrEmpty(livro.CapaPath))
            {
                livro.CapaPath = _livroRepository.Find(id).CapaPath;
            }
            else
            {
                var capaPath = _livroRepository.Find(id).CapaPath;
                var folderName = Path.Combine("Storage", "Images");
                String filepath = Path.Combine(folderName, capaPath);

                if (System.IO.File.Exists(filepath))
                {
                    System.IO.File.Delete(filepath);

                }

                var extensao = livro.CapaPath.Split(".");
                livro.CapaPath = RandomStr(10) + "." + extensao[extensao.Length - 1];
            }

            var _livro = _livroRepository.Find(id);
            if (_livro == null)
            {
                return NotFound();
            }

            try
            {
                _livroRepository.Update(livro);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_livroRepository.Exists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetLivros", new { id = livro.Id }, livro);
        }

        // POST: api/Livro
        [HttpPost]
        public IActionResult PostLivros([FromBody] Livro livro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var extensao = livro.CapaPath.Split(".");
            livro.CapaPath = RandomStr(10) + "." + extensao[extensao.Length - 1];

            _livroRepository.Add(livro);

            return CreatedAtAction("GetLivros", new { id = livro.Id }, livro);
        }

        // POST: api/Livro/Upload
        [HttpPost("Upload/{id}"), DisableRequestSizeLimit]
        public IActionResult Upload(long id)
        {
            try
            {
                Livro livro = _livroRepository.Find(id);

                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Storage", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    //var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fileName = livro.CapaPath;
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        // DELETE: api/Livro/5
        [HttpDelete("{id}")]
        public IActionResult DeleteLivros([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var livro = _livroRepository.Find(id);
            if (livro == null)
            {
                return NotFound();
            }

            var capaPath = livro.CapaPath;
            var folderName = Path.Combine("Storage", "Images");
            String filepath = Path.Combine(folderName, capaPath);

            if (System.IO.File.Exists(filepath))
            {
                System.IO.File.Delete(filepath);

            }

            _livroRepository.Remove(livro.Id);

            return Ok(livro);
        }

        public static string RandomStr(int length)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            return finalString;
        }
    }
}