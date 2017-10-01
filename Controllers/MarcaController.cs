using System.Collections.Generic;
using System.Linq;
using CatalogoAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoAPI.Controllers
{
    [Route("api/[controller]")]
    public class MarcaController : Controller
    {
        private readonly CatalogoContext _context;
        
        public MarcaController(CatalogoContext context)
        {
            _context = context;

            if (_context.Marcas.Count() == 0)
            {
                Marca toyota = new Marca {Nome = "Toyota"};
                _context.SaveChanges();
            }
        }

        [HttpGet(Name = "GetMarcas")]
        public IEnumerable<Marca> GetAll()
        {
            return _context.Marcas.ToList();
        }

        [HttpGet("{id}", Name = "GetMarca")]
        public IActionResult GetById(long id)
        {
            var item = _context.Marcas.FirstOrDefault(t => t.MarcaId == id);
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost("list/")]
        public IActionResult Create([FromBody] List<Marca> Marcas)
        {
            if (Marcas == null)
            {
                return BadRequest();
            }

            foreach (var Marca in Marcas) {
                _context.Marcas.Add(Marca);
            }

            _context.SaveChanges();
            return CreatedAtRoute("GetMarcas", null);
            // bug https://github.com/Microsoft/aspnet-api-versioning/issues/18
            //return CreatedAtRoute("GetMarca", new { id = marca.Id });
        }

        [HttpPost]
        public IActionResult Create([FromBody] Marca Marca)
        {
            if (Marca == null)
            {
                return BadRequest();
            }

            _context.Marcas.Add(Marca);
            _context.SaveChanges();

            return CreatedAtRoute("GetMarcas", null);
            // bug https://github.com/Microsoft/aspnet-api-versioning/issues/18
            //return CreatedAtRoute("GetMarca", new { id = marca.Id });
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Marca marca)
        {
            if (marca == null || marca.MarcaId != id)
            {
                return BadRequest();
            }

            var marcaOld = _context.Marcas.FirstOrDefault(t => t.MarcaId == id);
            if (marcaOld == null)
            {
                return NotFound();
            }

            marcaOld.Nome = marca.Nome;

            _context.Marcas.Update(marcaOld);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var marca = _context.Marcas.FirstOrDefault(t => t.MarcaId == id);
            if (marca == null)
            {
                return NotFound();
            }

            _context.Marcas.Remove(marca);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [Route("{id}/modelos")]
        public IActionResult GetModelos(long id) 
        {
            var Marcas = _context.Marcas;
            List<Modelo> Modelos = null;

            foreach (var item in Marcas) 
            {
                Modelos = _context.Modelos.Where(Modelo => Modelo.MarcaId == item.MarcaId).ToList(); 
            }

            return new ObjectResult(Modelos);
        } 
    }
}