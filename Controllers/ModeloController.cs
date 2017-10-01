using System.Collections.Generic;
using System.Linq;
using CatalogoAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace CatalogoAPI.Controllers
{
    [Route("api/[controller]")]
    public class ModeloController : Controller
    {
        private readonly CatalogoContext _context;

        public ModeloController(CatalogoContext context)
        {
            _context = context;

            //if (_context.Modelos.Count() == 0)
           // {
            //    Modelo modelo = new Modelo { Nome = "Corolla", MarcaId = 1 };
           //     modelo.Marca = _context.Marcas.FirstOrDefault(t => t.MarcaId == modelo.MarcaId);
//
            //    _context.Modelos.Add(modelo);
            //    _context.SaveChanges();
         //   }
        }

        [HttpGet(Name = "GetModelos")]
        public IEnumerable<Modelo> GetAll()
        {
            var items = _context.Modelos;
            
            foreach (Modelo item in items) {
                item.Marca = _context.Marcas.FirstOrDefault(Marca => Marca.MarcaId == item.MarcaId);
            }

            return items.ToList();
        }

        [HttpGet("{id}", Name = "GetModelo")]
        public IActionResult GetById(long id)
        {
            var item = _context.Modelos.FirstOrDefault(t => t.ModeloId == id);

            item.Marca = _context.Marcas.FirstOrDefault(Marca => Marca.MarcaId == item.MarcaId);

            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Modelo Modelo)
        {
            if (Modelo == null)
            {
                return BadRequest();
            }

            // Busca a marca com a marca id recebida
            Modelo.Marca = _context.Marcas.FirstOrDefault(Marca => Marca.MarcaId == Modelo.MarcaId);
            _context.Modelos.Add(Modelo);

            _context.SaveChanges();

            return CreatedAtRoute("GetModelos", null);
            // bug https://github.com/Microsoft/aspnet-api-versioning/issues/18
            //return CreatedAtRoute("GetModelo", new { id = Modelo.Id });
        }

        [HttpPost("list/")]
        public IActionResult Create([FromBody] List<Modelo> Modelos)
        {
            if (Modelos == null)
            {
                return BadRequest();
            }

            // Busca a marca com a marca id recebida
            foreach (var Modelo in Modelos)
            {
                _context.Modelos.Add(Modelo);
            }
            _context.SaveChanges();

            return CreatedAtRoute("GetModelos", null);
            // bug https://github.com/Microsoft/aspnet-api-versioning/issues/18
            //return CreatedAtRoute("GetModelo", new { id = Modelo.Id });
        }


        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] Modelo Modelo)
        {
            if (Modelo == null || Modelo.ModeloId != id)
            {
                return BadRequest();
            }

            var ModeloOld = _context.Modelos.FirstOrDefault(t => t.ModeloId == id);
            if (ModeloOld == null)
            {
                return NotFound();
            }

            ModeloOld.Nome = Modelo.Nome;
            ModeloOld.MarcaId = Modelo.MarcaId;
            ModeloOld.Marca = Modelo.Marca;

            _context.Modelos.Update(ModeloOld);
            _context.SaveChanges();
            return new NoContentResult();
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var Modelo = _context.Modelos.FirstOrDefault(t => t.ModeloId == id);
            if (Modelo == null)
            {
                return NotFound();
            }

            _context.Modelos.Remove(Modelo);
            _context.SaveChanges();
            return new NoContentResult();
        }
    }
}