using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LindaSonrisa.Data;
using LindaSonrisa.Models;
using Microsoft.AspNetCore.Identity;
using LindaSonrisa.Models.Views;
using LindaSonrisa.Models.Context;

namespace LindaSonrisa.Controllers
{
    public class ProveedoresController : Controller
    {
        private readonly LindaSonrisaContext _context;

        public ProveedoresController(LindaSonrisaContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("Proveedores")]
        public async Task<IActionResult> Index()
        {
            ViewData["RubroId"] = new SelectList(_context.Set<Rubro>().OrderBy(r => r.Titulo), "Id", "Titulo");
            ViewData["Proveedores"] = await _context.Proveedor.ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Proveedor proveedor)
        {
            if (ModelState.IsValid)
            {
                if (await _context.Proveedor.FirstOrDefaultAsync(p => p.NombreNormalizado == proveedor.NombreNormalizado) != null)
                {
                    return Json(new { success = false, errors = new List<string>() { "El proveedor (" + proveedor.Nombre + ") se encuentra registrado." }, message = "Se detectó 1 error." });
                }
                else
                {
                    proveedor.EstaInactivo = false;
                    proveedor.NombreNormalizado = proveedor.NombreNormalizado.Normalize();
                    _context.Add(proveedor);
                    await _context.SaveChangesAsync();
                    return Json(new { success = true, title = "El proveedor se ha creado!" });
                }

            }
            return Json(new { success = false, errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList(), message = "Se detectó " + ModelState.Values.SelectMany(x => x.Errors).Count() + " error(es)." });
        }

        [HttpGet]
        public async Task<IActionResult> Get(int? id)
        {
            Proveedor model = await _context.Proveedor.FindAsync(id);
            if (model == null)
            {
                return Json(new { success = false, message = "El proveedor no se encontró" });
            }

            return Json(new { success = true, proveedor = model });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<Proveedor> proveedores = await _context.Proveedor.ToListAsync();
            if (proveedores.Count() == 0)
            {
                return Json(new { success = false, message = "No se encontró proveedores" });
            }

            return Json(new { success = true, proveedores = proveedores });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllForOrden()
        {
            List<Proveedor> proveedores = await _context.Proveedor.Where(p => p.Producto.Count() > 0).OrderBy(p => p.Nombre).ToListAsync();

            if (proveedores.Count() == 0)
            {
                return Json(new { success = false, message = "No se encontró proveedores que tengan productos" });
            }

            return Json(new { success = true, proveedores = proveedores });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Proveedor model)
        {
            Proveedor proveedor = await _context.Proveedor.FindAsync(model.Id);

            if (proveedor is null)
            {
                return Json(new { success = true, errors = new List<string>() { "No se encontró el proveedor." }, message = "Se detectó 1 error." });
            }

            if (proveedor.NombreNormalizado != model.NombreNormalizado)
            {
                if (await _context.Proveedor.FirstOrDefaultAsync(p => p.NombreNormalizado == model.NombreNormalizado) != null)
                {
                    return Json(new { success = false, errors = new List<string>() { "El proveedor (" + model.Nombre + ") se encuentra registrado." }, message = "Se detectó 1 error." });
                }
            }

            if (ModelState.IsValid)
            {
                proveedor.Nombre = model.Nombre;
                proveedor.NombreNormalizado = model.NombreNormalizado.Normalize();
                proveedor.FonoFijo = model.FonoFijo;
                proveedor.FonoMovil = model.FonoMovil;
                proveedor.Email = model.Email;
                proveedor.Url = model.Url;
                proveedor.Comentario = model.Comentario;
                proveedor.RubroId = model.RubroId;

                _context.Update(proveedor);
                await _context.SaveChangesAsync();
                return Json(new { success = true, title = "El proveedor se ha modificado!" });
            }
            return Json(new { success = false, errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList(), message = "Se detectó " + ModelState.Values.SelectMany(x => x.Errors).Count() + " error(es)." });
        }

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Proveedor proveedor = await _context.Proveedor.Include(p => p.Contacto).Include(p => p.Producto).FirstOrDefaultAsync(s => s.Id == id);

            if (proveedor is null)
            {
                return Json(new { success = false, title = "Oops...no se encontró el proveedor" });
            }

            if (proveedor.Contacto.Count() > 0 || proveedor.Producto.Count() > 0)
            {
                return Json(new { success = false, title = "Oops...existen registros asociados al proveedor" });
            }

            _context.Proveedor.Remove(proveedor);
            await _context.SaveChangesAsync();
            return Json(new { success = true, title = "El proveedor se ha eliminado!" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeStatus(int id)
        {
            Proveedor proveedor = await _context.Proveedor.FindAsync(id);
            if (proveedor == null)
            {
                return Json(new { success = false, title = "Oops...no se encontró el proveedor" });
            }

            if (!proveedor.EstaInactivo)
            {
                proveedor.EstaInactivo = true;
            }
            else
            {
                proveedor.EstaInactivo = false;
            }

            _context.Update(proveedor);
            await _context.SaveChangesAsync();
            return Json(new { success = true, title = "El proveedor ha cambiado de estado!" });
        }
    }
}
