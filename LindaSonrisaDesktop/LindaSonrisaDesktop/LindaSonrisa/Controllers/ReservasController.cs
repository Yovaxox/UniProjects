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
using LindaSonrisa.Models.Identity;
using LindaSonrisa.Models.Views;
using System.Globalization;

namespace LindaSonrisa.Controllers
{
    public class ReservasController : Controller
    {
        private readonly LindaSonrisaContext _context;
        private readonly UserManager<User> _userManager;

        public ReservasController(LindaSonrisaContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("Reservas")]
        public async Task<IActionResult> Index()
        {
            List<Option> options = new List<Option>();

            foreach (Modulo modulo in await _context.Modulo.Include(m => m.Servicio).Where(m => m.Disponible == '1').ToListAsync())
            {
                Option option = new Option()
                {
                    Value = modulo.ServicioId,
                    Text = modulo.Servicio.Titulo
                };

                options.Add(option);
            }

            ViewData["ServicioId"] = new SelectList(options, "Value", "Text");
            ViewData["Reservas"] = await _context.Reserva.Include(r => r.Modulo.Servicio).Include(r => r.Modulo.Usuario).Include(r => r.Usuario).ToListAsync();
            return View();
        }

        private class Item
        {
            public string Fecha { get; set; }
            public string Estado { get; set; }

            public bool IsDisabled { get; set; }
        }

        public async Task<IActionResult> GetByModulos(int id)
        {
            Modulo modulo = await _context.Modulo.FindAsync(id);
            if (modulo is null)
            {
                return Json(new { success = false, message = "El módulo no se encontró" });
            }

            DateTime nextModulo = GetNextWeekday(DateTime.Today.AddDays(1), (DayOfWeek)modulo.DiaId);

            List<Reserva> reservas = await _context.Reserva.Where(r => r.ModuloId == modulo.Id).ToListAsync();
            List<Item> items = new List<Item>();

            for (DateTime date = nextModulo; date < nextModulo.AddMonths(6); date = date.AddDays(7))
            {
                Item item = new Item()
                {
                    Fecha = date.ToString("dd/MM/yyyy"),
                    Estado = "Disponible",
                    IsDisabled = false,
                };

                foreach (Reserva reserva in reservas)
                {
                    if (date.ToString("dd/MM/yyyy") == reserva.FechaReserva.ToString("dd/MM/yyyy"))
                    {
                        if (reserva.FueAnulada == '0')
                        {
                            item.Estado = "No disponible";
                            item.IsDisabled = true;
                        }
                    }
                }

                items.Add(item);
            }

            return Json(new { success = true, data = items });
        }

        public static DateTime GetNextWeekday(DateTime start, DayOfWeek day)
        {
            int daysToAdd = ((int)day - (int)start.DayOfWeek + 7) % 7;
            return start.AddDays(daysToAdd);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string fecha, Reserva reserva)
        {
            if (reserva.UsuarioId == 0)
            {
                ModelState.AddModelError(string.Empty, "No ha seleccionado un cliente.");
                return Json(new { success = false, errors = new List<string>() { "No ha seleccionado un cliente." }, message = "Se detectó 1 error" });
            }

            if (ModelState.IsValid)
            {
                Modulo modulo = await _context.Modulo.FindAsync(reserva.ModuloId); ;
                if (modulo is null)
                {
                    return Json(new { success = false, message = "El módulo no se encontró" });
                }

                reserva.FechaReserva = DateTime.ParseExact(fecha, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                reserva.SolicitadoEl = DateTime.Now;
                reserva.FueAnulada = '0';

                _context.Add(reserva);
                await _context.SaveChangesAsync();
                return Json(new { success = true, title = "La reserva se ha creado!" });
            }

            return Json(new { success = false, errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList(), message = "Se detectó " + ModelState.Values.SelectMany(x => x.Errors).Count() + " error(es)." });
        }

        [HttpGet]
        public async Task<IActionResult> Get(int? id)
        {
            Reserva reserva = await _context.Reserva.Include(r => r.Usuario).Include(r => r.Modulo.Usuario).Include(r => r.Modulo.Servicio).Where(r => r.Id == id).FirstOrDefaultAsync();
            if (reserva == null)
            {
                return Json(new { success = false, message = "La reserva no se encontró" });
            }

            var item = new
            {
                FechaReserva = reserva.FechaReserva.ToString("dd/MM/yyyy"),
                FechaSolicitado = reserva.SolicitadoEl.ToString("dd/MM/yyyy hh:mm"),
                FechaAnulacion = reserva.FechaAnulacion.HasValue ? reserva.FechaAnulacion.Value.ToString("dd/MM/yyyy hh:mm") : "n/a",
                Odontologo = reserva.Modulo.Usuario.Nombre + " " + reserva.Modulo.Usuario.ApPaterno + " " + reserva.Modulo.Usuario.ApMaterno,
                Cliente = reserva.Usuario.Nombre + " " + reserva.Usuario.ApPaterno + " " + reserva.Usuario.ApMaterno,
                Servicio = reserva.Modulo.Servicio.Titulo,
                Modulo = reserva.ModuloId,
                HoraReserva = reserva.Modulo.HoraInicio,
            };

            return Json(new { success = true, data = item });
        }

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Reserva reserva = await _context.Reserva.FirstOrDefaultAsync(s => s.Id == id);

            if (reserva is null)
            {
                return Json(new { success = false, title = "Oops...no se encontró la reserva" });
            }

            _context.Reserva.Remove(reserva);
            await _context.SaveChangesAsync();
            return Json(new { success = true, title = "La reserva se ha eliminado!" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Anular(int id)
        {
            Reserva reserva = await _context.Reserva.FindAsync(id);
            if (reserva == null)
            {
                return Json(new { success = false, title = "Oops...no se encontró la reserva" });
            }

            if (reserva.FueAnulada == '1')
            {
                return Json(new { success = false, title = "El reserva ya fue anulada" });
            }

            reserva.FueAnulada = '1'; 
            _context.Update(reserva);
            await _context.SaveChangesAsync();
            return Json(new { success = true, title = "La reserva se anuló!" });
        }
    }
}
