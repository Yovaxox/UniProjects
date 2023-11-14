using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LindaSonrisa.Data;
using LindaSonrisa.Models.Context;
using Microsoft.AspNetCore.Identity;
using LindaSonrisa.Models.Views;
using LindaSonrisa.Models.Identity;
using Servicio = LindaSonrisa.Models.Context.Servicio;

namespace LindaSonrisa.Controllers
{
    public class ModulosController : Controller
    {
        private readonly LindaSonrisaContext _context;
        private readonly UserManager<User> _userManager;

        public ModulosController(LindaSonrisaContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("Modulos")]
        public async Task<IActionResult> Index()
        {
            List<Option> options = new List<Option>();

            foreach (var usuario in await _context.Usuario.Include(u => u.User).Where(u => u.UserId != null).ToListAsync())
            {
                foreach (var role in await _userManager.GetRolesAsync(usuario.User))
                {
                    if (role == "Odontólogo")
                    {
                        options.Add(new Option() { Value = usuario.Id, Text = usuario.Nombre + " " + usuario.ApPaterno + " " + usuario.ApMaterno });
                    }
                }
            }

            ViewData["ServicioId"] = new SelectList(_context.Set<Models.Context.Servicio>().Where(s => s.EstaInactivo == '0'), "Id", "Titulo");
            ViewData["DiaId"] = new SelectList(_context.Set<Dia>().Where(d => d.Id != 0), "Id", "Titulo");
            ViewData["UsuarioId"] = new SelectList(options, "Value", "Text");
            ViewData["Modulos"] = await _context.Modulo.Include(m => m.Dia).Include(m => m.Servicio).Include(m => m.Usuario).ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Modulo modulo)
        {
            if (ModuloExists(modulo))
            {
                return Json(new { success = false, errors = new List<string>() { "El módulo ya existe." }, message = "Se detectó 1 error." });
            }

            if (TimeSpan.Parse(modulo.HoraTermino) <= TimeSpan.Parse(modulo.HoraInicio))
            {
                return Json(new { success = false, errors = new List<string>() { "La hora de término debe ser mayor a la hora de inicio." }, message = "Se detectó 1 error." });
            }

            if (ModelState.IsValid)
            {
                modulo.Disponible = '1';
                _context.Add(modulo);
                await _context.SaveChangesAsync();
                return Json(new { success = true, title = "El módulo se ha creado!" });
            }
            return Json(new { success = false, errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList(), message = "Se detectó " + ModelState.Values.SelectMany(x => x.Errors).Count() + " error(es)." });
        }

        [HttpGet]
        public async Task<IActionResult> Get(int? id)
        {
            Modulo model = await _context.Modulo.FindAsync(id);
            if (model == null)
            {
                return Json(new { success = false, message = "El módulo no se encontró" });
            }

            return Json(new { success = true, modulo = model });
        }

        [HttpGet]
        public async Task<IActionResult> GetByServicio(int id)
        {
            Servicio servicio = await _context.Servicio.FindAsync(id); ;
            if (servicio is null)
            {
                return Json(new { success = false, message = "El servicio no se encontró" });
            }

            List<Option> options = new List<Option>();

            foreach (Modulo modulo in await _context.Modulo.Include(m => m.Dia).Where(m => m.ServicioId == id && m.Disponible == '1').OrderBy(m => m.Dia.Titulo).OrderBy(m => m.HoraInicio).ToListAsync())
            {
                options.Add(new Option() { Value = modulo.Id, Text = modulo.HoraInicio + " - " + modulo.Dia.Titulo });
            }

            return Json(new { success = true, modulos = options });
        }

        [HttpGet]        
        public async Task<IActionResult> GetByOdontologo(int odontologoId, int servicioId)
        {
            Usuario usuario = await _context.Usuario.FindAsync(odontologoId); ;
            if (usuario is null)
            {
                return Json(new { success = false, message = "El odontólogo no se encontró" });
            }

            List<Option> options = new List<Option>();

            foreach (Modulo modulo in await _context.Modulo.Include(m => m.Dia).Where(m => m.UsuarioId == odontologoId && m.ServicioId == servicioId && m.Disponible == '1').OrderBy(m => m.Dia.Titulo).OrderBy(m => m.HoraInicio).ToListAsync())
            {
                options.Add(new Option() { Value = modulo.Id, Text = modulo.HoraInicio + " - " + modulo.Dia.Titulo });
            }

            return Json(new { success = true, modulos = options });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Modulo model)
        {
            Modulo modulo = await _context.Modulo.FindAsync(model.Id);

            if (modulo is null)
            {
                return Json(new { success = true, errors = new List<string>() { "No se encontró el módulo." }, message = "Se detectó 1 error." });
            }

            if (TimeSpan.Parse(model.HoraTermino) <= TimeSpan.Parse(model.HoraInicio))
            {
                return Json(new { success = false, errors = new List<string>() { "La hora de término debe ser mayor a la hora de inicio." }, message = "Se detectó 1 error." });
            }

            if (ModelState.IsValid)
            {
                modulo.HoraInicio = model.HoraInicio;
                modulo.HoraTermino = model.HoraTermino;
                modulo.ServicioId = model.ServicioId;
                modulo.DiaId = model.DiaId;
                modulo.Box = model.Box;
                modulo.UsuarioId = model.UsuarioId;

                _context.Update(modulo);
                await _context.SaveChangesAsync();
                return Json(new { success = true, title = "El módulo se ha modificado!" });
            }
            return Json(new { success = false, errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList(), message = "Se detectó " + ModelState.Values.SelectMany(x => x.Errors).Count() + " error(es)." });
        }

        private bool ModuloExists(Modulo model)
        {
            Modulo modulo = _context.Modulo.Where(m => m.UsuarioId == model.UsuarioId && m.ServicioId == model.ServicioId && m.DiaId == model.DiaId && m.HoraInicio == model.HoraInicio).FirstOrDefault();

            if (modulo != null)
            {
                return true;
            }

            return false;
        }

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Modulo modulo = await _context.Modulo.Include(m => m.Reserva).FirstOrDefaultAsync(s => s.Id == id);

            if (modulo is null)
            {
                return Json(new { success = false, title = "Oops...no se encontró el módulo" });
            }

            if (modulo.Reserva.Count() > 0)
            {
                return Json(new { success = false, title = "Oops...existen registros asociados al módulo" });
            }

            _context.Modulo.Remove(modulo);
            await _context.SaveChangesAsync();
            return Json(new { success = true, title = "El módulo se ha eliminado!" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeStatus(int id)
        {
            Modulo modulo = await _context.Modulo.FindAsync(id);
            if (modulo == null)
            {
                return Json(new { success = false, title = "Oops...no se encontró el módulo" });
            }

            if (modulo.Disponible == '0')
            {
                modulo.Disponible = '1';
            }
            else
            {
                modulo.Disponible = '0';
            }

            _context.Update(modulo);
            await _context.SaveChangesAsync();
            return Json(new { success = true, title = "El módulo ha cambiado de estado!" });
        }
    }
}
