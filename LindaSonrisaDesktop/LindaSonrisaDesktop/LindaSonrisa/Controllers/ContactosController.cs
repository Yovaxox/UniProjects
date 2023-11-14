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
    public class ContactosController : Controller
    {
        private readonly LindaSonrisaContext _context;

        public ContactosController(LindaSonrisaContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("Contactos")]
        public async Task<IActionResult> Index()
        {
            ViewData["Contactos"] = await _context.Contacto.Include(c => c.Proveedor).ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Contacto contacto)
        {
            if (ModelState.IsValid)
            {
                if (await _context.Contacto.FirstOrDefaultAsync(p => p.FonoMovil == contacto.FonoMovil) != null)
                {
                    return Json(new { success = false, errors = new List<string>() { "El contacto con telefono móvil (" + contacto.FonoMovil + ") se encuentra registrado." }, message = "Se detectó 1 error." });
                }
                else
                {
                    _context.Add(contacto);
                    await _context.SaveChangesAsync();
                    return Json(new { success = true, title = "El contacto se ha creado!" });
                }

            }
            return Json(new { success = false, errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList(), message = "Se detectó " + ModelState.Values.SelectMany(x => x.Errors).Count() + " error(es)." });
        }

        [HttpGet]
        public async Task<IActionResult> Get(int? id)
        {
            Contacto model = await _context.Contacto.FindAsync(id);
            if (model == null)
            {
                return Json(new { success = false, message = "El contacto no se encontró" });
            }

            return Json(new { success = true, contacto = model });
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Contacto model)
        {
            Contacto contacto = await _context.Contacto.FindAsync(model.Id);

            if (contacto is null)
            {
                return Json(new { success = true, errors = new List<string>() { "No se encontró el contacto." }, message = "Se detectó 1 error." });
            }

            if (contacto.FonoMovil != model.FonoMovil)
            {
                if (await _context.Contacto.FirstOrDefaultAsync(c => c.FonoMovil == model.FonoMovil) != null)
                {
                    return Json(new { success = false, errors = new List<string>() { "El contacto con telefono móvil (" + contacto.FonoMovil + ") se encuentra registrado." }, message = "Se detectó 1 error." });
                }
            }

            if (ModelState.IsValid)
            {
                contacto.Nombre = model.Nombre;
                contacto.ApPaterno = model.ApPaterno;
                contacto.ApMaterno = model.ApMaterno;
                contacto.FonoMovil = model.FonoMovil;
                contacto.Email = model.Email;
                contacto.ProveedorId = model.ProveedorId;

                _context.Update(contacto);
                await _context.SaveChangesAsync();
                return Json(new { success = true, title = "El contacto se ha modificado!" });
            }
            return Json(new { success = false, errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList(), message = "Se detectó " + ModelState.Values.SelectMany(x => x.Errors).Count() + " error(es)." });
        }

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Contacto contacto = await _context.Contacto.FirstOrDefaultAsync(s => s.Id == id);

            if (contacto is null)
            {
                return Json(new { success = false, title = "Oops...no se encontró el contacto" });
            }

            _context.Contacto.Remove(contacto);
            await _context.SaveChangesAsync();
            return Json(new { success = true, title = "El contacto se ha eliminado!" });
        }
    }
}
