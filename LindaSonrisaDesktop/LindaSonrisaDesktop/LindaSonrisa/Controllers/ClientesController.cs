using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LindaSonrisa.Data;
using LindaSonrisa.Models.Context;
using LindaSonrisa.Models.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace LindaSonrisa.Controllers
{
    public class ClientesController : Controller
    {
        private readonly LindaSonrisaContext _context;

        public ClientesController(LindaSonrisaContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("Clientes")]
        public async Task<IActionResult> Index()
        {
            ViewData["Clientes"] = await _context.AuthUser
                .Include(u => u.Usuario).Include(u => u.Usuario.Comuna)
                .Include(u => u.Usuario.EstadoCivil).Include(u => u.Usuario.Genero)
                .Where(u => u.IsActive == true)
                .ToListAsync();

            ViewData["RegionId"] = new SelectList(_context.Set<Region>(), "Id", "Titulo");
            ViewData["EstadoCivilId"] = new SelectList(_context.Set<EstadoCivil>(), "Id", "Titulo");
            ViewData["GeneroId"] = new SelectList(_context.Set<Genero>(), "Id", "Titulo");

            return View();
        }

        public async Task<IActionResult> Create(Cliente cliente)
        {
            if (!ValidateRun(cliente.Run))
            {
                ModelState.AddModelError(string.Empty, "El RUN ingresado no es válido.");
                return Json(new { success = false, errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList(), message = "Se detectó " + ModelState.Values.SelectMany(x => x.Errors).Count() + " error(es)." });
            }

            if (ModelState.IsValid)
            {
                if (await _context.AuthUser.Where(a => a.UserName == cliente.Run).FirstOrDefaultAsync() != null)
                {
                    ModelState.AddModelError(string.Empty, "El cliente con el RUN ingresado ya está registrado.");
                    return Json(new { success = false, errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList(), message = "Se detectó " + ModelState.Values.SelectMany(x => x.Errors).Count() + " error(es)." });
                }

                if (await _context.AuthUser.Where(a => a.Email == cliente.Email).FirstOrDefaultAsync() != null)
                {
                    ModelState.AddModelError(string.Empty, "El cliente con el email ingresado ya está registrado.");
                    return Json(new { success = false, errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList(), message = "Se detectó " + ModelState.Values.SelectMany(x => x.Errors).Count() + " error(es)." });
                }

                AuthUser authUser = new AuthUser
                {
                    UserName = cliente.Run,
                    Email = cliente.Email,
                    IsSuperUser = false,
                    IsStaff = false,
                    IsActive = true,
                    DateJoined = DateTime.Now
                };

                _context.Add(authUser);
                await _context.SaveChangesAsync();

                authUser = await _context.AuthUser.Where(a => a.UserName == authUser.UserName).FirstOrDefaultAsync();

                Usuario usuario = new Usuario()
                {
                    Nombre = cliente.Nombre,
                    ApPaterno = cliente.ApPaterno,
                    ApMaterno = cliente.ApMaterno,
                    FechaNacimiento = cliente.FechaNacimiento,
                    FonoFijo = cliente.FonoFijo,
                    FonoMovil = cliente.FonoMovil,
                    Direccion = cliente.Direccion,
                    EsExtranjero = cliente.EsExtranjero,
                    CreadoEl = authUser.DateJoined,
                    ComunaId = cliente.ComunaId,
                    GeneroId = cliente.GeneroId,
                    EstadoCivilId = cliente.EstadoCivilId,
                    AuthUserId = authUser.Id
                };

                _context.Add(usuario);
                await _context.SaveChangesAsync();

                return Json(new { success = true, title = "El cliente se ha creado!" });
            }

            return Json(new { success = false, errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList(), message = "Se detectó " + ModelState.Values.SelectMany(x => x.Errors).Count() + " error(es)." });
        }

        public static bool ValidateRun(string run)
        {
            run = run.Replace(".", "").ToUpper();
            Regex expresion = new Regex("^([0-9]+-[0-9K])$");
            string dv = run.Substring(run.Length - 1, 1);
            if (!expresion.IsMatch(run))
            {
                return false;
            }
            char[] charCorte = { '-' };
            string[] runTemp = run.Split(charCorte);
            if (dv != Digito(int.Parse(runTemp[0])))
            {
                return false;
            }
            return true;
        }

        public static string Digito(int run)
        {
            int suma = 0;
            int multiplicador = 1;
            while (run != 0)
            {
                multiplicador++;
                if (multiplicador == 8)
                    multiplicador = 2;
                suma += (run % 10) * multiplicador;
                run = run / 10;
            }
            suma = 11 - (suma % 11);
            if (suma == 11)
            {
                return "0";
            }
            else if (suma == 10)
            {
                return "K";
            }
            else
            {
                return suma.ToString();
            }
        }

        public async Task<ActionResult> GetAllForReserva()
        {
            List<Cliente> clientes = new List<Cliente>();

            foreach (AuthUser authUser in await _context.AuthUser.Include(a => a.Usuario).Where(a => a.IsActive == true).ToListAsync())
            {
                Cliente cliente = new Cliente()
                {
                    Id = authUser.Usuario.Id,
                    Run = authUser.UserName,
                    Nombre = authUser.Usuario.Nombre,
                    ApPaterno = authUser.Usuario.ApPaterno,
                    ApMaterno = authUser.Usuario.ApMaterno  
                };

                clientes.Add(cliente);
            }

            return Json(new { success = true, data = clientes });
        }

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            AuthUser authUser = await _context.AuthUser.Include(a => a.Usuario.Reserva).FirstOrDefaultAsync(u => u.Id == id);

            if (authUser is null)
            {
                return Json(new { success = false, title = "El cliente no se encontró" });
            }

            if (authUser.Usuario.Reserva.Count() > 0)
            {
                return Json(new { success = false, title = "Oops...existen registros asociados al cliente" });
            }

            _context.Usuario.Remove(authUser.Usuario);
            await _context.SaveChangesAsync();
            _context.AuthUser.Remove(authUser);
            await _context.SaveChangesAsync();

            return Json(new { success = true, title = "El cliente se ha eliminado!" });
        }
    }
}
