using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LindaSonrisa.Data;
using LindaSonrisa.Models.Context;
using LindaSonrisa.Models.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LindaSonrisa.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly LindaSonrisaContext _context;

        public CategoriasController(LindaSonrisaContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("Categorias")]
        public async Task<IActionResult> Index()
        {
            ViewData["TipoProductos"] = await _context.TipoProducto.Include(t => t.FamiliaProducto).ToListAsync();
            ViewData["FamiliaProductos"] = await _context.FamiliaProducto.ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                if (categoria.CategoriaId == 0)
                {
                    if (await _context.TipoProducto.FirstOrDefaultAsync(t => t.TituloNormalizado == categoria.TituloNormalizado.Normalize()) != null)
                    {
                        return Json(new { success = false, errors = new List<string>() { "El tipo de producto (" + categoria.Titulo + ") se encuentra registrado." }, message = "Se detectó 1 error." });
                    }
                    else
                    {
                        TipoProducto tipoProducto = new TipoProducto()
                        {
                            Titulo = categoria.Titulo,
                            TituloNormalizado = categoria.TituloNormalizado.Normalize(),
                            FamiliaProductoId = (int)categoria.FamiliaProductoId,
                        };
                        _context.Add(tipoProducto);
                        await _context.SaveChangesAsync();
                        return Json(new { success = true, title = "La categoría se ha creado!" });
                    }
                }

                if (categoria.CategoriaId == 1)
                {
                    if (await _context.FamiliaProducto.FirstOrDefaultAsync(f => f.TituloNormalizado == categoria.TituloNormalizado.Normalize()) != null)
                    {
                        return Json(new { success = false, errors = new List<string>() { "La familia de producto (" + categoria.Titulo + ") se encuentra registrado." }, message = "Se detectó 1 error." });
                    }
                    else
                    {
                        FamiliaProducto familiaProducto = new FamiliaProducto()
                        {
                            Titulo = categoria.Titulo,
                            TituloNormalizado = categoria.TituloNormalizado.Normalize(),
                        };
                        _context.Add(familiaProducto);
                        await _context.SaveChangesAsync();
                        return Json(new { success = true, title = "La categoría se ha creado!" });
                    }
                }
            }
            return Json(new { success = false, errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList(), message = "Se detectó " + ModelState.Values.SelectMany(x => x.Errors).Count() + " error(es)." });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                if (categoria.CategoriaId == 0)
                {
                    TipoProducto tipoProducto = await _context.TipoProducto.FindAsync(categoria.Id);

                    if (tipoProducto is null)
                    {
                        return Json(new { success = false, message = "No se encontró el tipo de producto" });
                    }

                    if (tipoProducto.TituloNormalizado != categoria.TituloNormalizado)
                    {
                        if (await _context.TipoProducto.FirstOrDefaultAsync(t => t.TituloNormalizado == categoria.TituloNormalizado.Normalize()) != null)
                        {
                            return Json(new { success = false, errors = new List<string>() { "El tipo de producto (" + categoria.Titulo + ") se encuentra registrado." }, message = "Se detectó 1 error." });
                        }
                    }

                    tipoProducto.Titulo = categoria.Titulo;
                    tipoProducto.TituloNormalizado = categoria.TituloNormalizado.Normalize();
                    tipoProducto.FamiliaProductoId = (int)categoria.FamiliaProductoId;

                    _context.Update(tipoProducto);
                    await _context.SaveChangesAsync();
                    return Json(new { success = true, title = "La categoría se ha moficicado!" });
                }

                if (categoria.CategoriaId == 1)
                {
                    FamiliaProducto familiaProducto = await _context.FamiliaProducto.FindAsync(categoria.Id);

                    if (familiaProducto is null)
                    {
                        return Json(new { success = false, message = "No se encontró la familia de producto" });
                    }

                    if (familiaProducto.TituloNormalizado != categoria.TituloNormalizado)
                    {
                        if (await _context.TipoProducto.FirstOrDefaultAsync(t => t.TituloNormalizado == categoria.TituloNormalizado.Normalize()) != null)
                        {
                            return Json(new { success = false, errors = new List<string>() { "La familia de producto (" + categoria.Titulo + ") se encuentra registrado." }, message = "Se detectó 1 error." });
                        }
                    }

                    familiaProducto.Titulo = categoria.Titulo;
                    familiaProducto.TituloNormalizado = categoria.TituloNormalizado.Normalize();

                    _context.Update(familiaProducto);
                    await _context.SaveChangesAsync();
                    return Json(new { success = true, title = "La categoría se ha moficicado!" });
                }
            }
            return Json(new { success = false, errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList(), message = "Se detectó " + ModelState.Values.SelectMany(x => x.Errors).Count() + " error(es)." });
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id, int categoria)
        {
            if (categoria == 0)
            {
                TipoProducto tipoProducto = await _context.TipoProducto.FindAsync(id);

                if (tipoProducto is null)
                {
                    return Json(new { success = false, message = "No se encontró el tipo de producto" });
                }

                return Json(new { success = true, tipoProducto = tipoProducto });
            }

            if (categoria == 1)
            {
                FamiliaProducto familiaProducto = await _context.FamiliaProducto.FindAsync(id);

                if (familiaProducto is null)
                {
                    return Json(new { success = false, message = "No se encontró la familia de producto" });
                }

                return Json(new { success = true, familiaProducto = familiaProducto });
            }

            return Json(new { success = false, message = "No se encontró la categoría" });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int categoria)
        {
            if (categoria == 0)
            {
                List<TipoProducto> tipoProductos = await _context.TipoProducto.ToListAsync();
                if (tipoProductos.Count() == 0)
                {
                    return Json(new { success = false, message = "No se encontró tipos de producto" });
                }

                return Json(new { success = true, tipoProductos = tipoProductos });
            }

            if (categoria == 1)
            {
                List<FamiliaProducto> familiaProductos = await _context.FamiliaProducto.ToListAsync();
                if (familiaProductos.Count() == 0)
                {
                    return Json(new { success = false, message = "No se encontró familias de producto" });
                }

                return Json(new { success = true, familiaProductos = familiaProductos });
            }

            return Json(new { success = false, message = "No se encontró las categorías" });
        }

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, int categoria)
        {
            if (categoria == 0)
            {
                TipoProducto tipoProducto = await _context.TipoProducto.Include(f => f.Producto).FirstOrDefaultAsync(s => s.Id == id);

                if (tipoProducto is null)
                {
                    return Json(new { success = false, title = "Oops...no se ha encontrado la categoría" });
                }

                if (tipoProducto.Producto.Count() > 0)
                {
                    return Json(new { success = false, title = "La categoría seleccionada tiene registros asociados" });
                }

                _context.TipoProducto.Remove(tipoProducto);
                await _context.SaveChangesAsync();
                return Json(new { success = true, title = "La categoría seleccionada se ha eliminado!" });
            }
            else if (categoria == 1)
            {
                FamiliaProducto familiaProducto = await _context.FamiliaProducto.Include(f => f.TipoProducto).FirstOrDefaultAsync(s => s.Id == id);

                if (familiaProducto is null)
                {

                    return Json(new { success = false, title = "Oops...no se ha encontrado la categoría" });
                }

                if (familiaProducto.TipoProducto.Count() > 0)
                {
                    return Json(new { success = false, title = "La categoría seleccionada tiene registros asociados" });
                }

                _context.FamiliaProducto.Remove(familiaProducto);
                await _context.SaveChangesAsync();
                return Json(new { success = true, title = "La categoría seleccionada se ha eliminado!" });
            }

            return Json(new { success = false, title = "Oops...no se ha encontrado la categoría" });
        }
    }
}
