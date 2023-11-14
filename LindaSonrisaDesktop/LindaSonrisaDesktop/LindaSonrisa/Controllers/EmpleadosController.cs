using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LindaSonrisa.Data;
using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;
using LindaSonrisa.Models.Identity;
using LindaSonrisa.Models.Context;
using LindaSonrisa.Models.Views;
using Servicio = LindaSonrisa.Models.Context.Servicio;

namespace LindaSonrisa.Controllers
{
    public class EmpleadosController : Controller
    {
        private readonly LindaSonrisaContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public EmpleadosController(LindaSonrisaContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        [Route("Empleados")]
        public async Task<IActionResult> Index()
        {
            ViewData["Empleados"] = await _context.Usuario.Include(u => u.User).Include(u => u.Comuna).Include(u => u.EstadoCivil).Include(u => u.Genero).Where(u => u.User.UserName != User.Identity.Name && u.UserId != null).ToListAsync();
            ViewData["RegionId"] = new SelectList(_context.Set<Region>(), "Id", "Titulo");
            ViewData["EstadoCivilId"] = new SelectList(_context.Set<EstadoCivil>(), "Id", "Titulo");
            ViewData["GeneroId"] = new SelectList(_context.Set<Genero>(), "Id", "Titulo");
            ViewData["RoleId"] = new SelectList(_roleManager.Roles, "Id", "Name");
            ViewData["UserManager"] = _userManager;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Empleado empleado)
        {
            if (!ValidateRun(empleado.Run))
            {
                ModelState.AddModelError(string.Empty, "El RUN ingresado no es válido.");
                return Json(new { success = false, errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList(), message = "Se detectó " + ModelState.Values.SelectMany(x => x.Errors).Count() + " error(es)." });
            }

            if (ModelState.IsValid)
            {
                User user = new User
                {
                    UserName = empleado.Run,
                    Email = empleado.Email
                };

                IdentityResult identityResult = await _userManager.CreateAsync(user, empleado.Password);

                if (identityResult.Succeeded)
                {
                    IdentityRole identityRole = await _roleManager.FindByIdAsync(empleado.RoleId);

                    user = await _context.User.Where(u => u.Email == empleado.Email).FirstAsync();

                    IdentityResult identityResult2 = await _userManager.AddToRoleAsync(user, identityRole.Name);

                    if (identityResult2.Succeeded)
                    {
                        Usuario usuario = new Usuario
                        {
                            Nombre = empleado.Nombre,
                            ApPaterno = empleado.ApPaterno,
                            ApMaterno = empleado.ApMaterno,
                            FechaNacimiento = empleado.FechaNacimiento,
                            FonoFijo = empleado.FonoFijo,
                            FonoMovil = empleado.FonoMovil,
                            EstadoCivilId = empleado.EstadoCivilId,
                            Direccion = empleado.Direccion,
                            ComunaId = empleado.ComunaId,
                            GeneroId = empleado.GeneroId,
                            UserId = user.Id,
                            CreadoEl = DateTime.Now
                        };

                        _context.Add(usuario);
                        await _context.SaveChangesAsync();

                        return Json(new { success = true, title = "El empleado se ha creado!" });
                    }

                    foreach (IdentityError identityError in identityResult2.Errors)
                    {
                        ModelState.AddModelError(string.Empty, identityError.Description);
                    }
                }

                foreach (IdentityError identityError in identityResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, identityError.Description);
                }

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

        public async Task<IActionResult> Get(int? id)
        {
            Usuario usuario  = await _context.Usuario.Include(u => u.User).Include(u => u.Comuna).FirstAsync(u => u.Id == id);
            if (usuario == null)
            {
                return Json(new { success = false, message = "El empleado no se encontró" });
            }

            Empleado model = new Empleado
            {
                Id = usuario.Id,
                Email = usuario.User.Email,
                Run = usuario.User.UserName,
                Nombre = usuario.Nombre,
                ApPaterno = usuario.ApPaterno,
                ApMaterno = usuario.ApMaterno,
                FonoFijo = usuario.FonoFijo,
                FonoMovil = usuario.FonoMovil,
                GeneroId = usuario.GeneroId,
                EstadoCivilId = usuario.EstadoCivilId,
                ComunaId = usuario.ComunaId,
                RegionId = _context.Region.Find(usuario.Comuna.RegionId).Id,
                Direccion = usuario.Direccion,
                FechaNacimiento = usuario.FechaNacimiento.Date,
                StringOfCreadoEl = usuario.CreadoEl.Value.ToString("dd/MM/yyyy HH:mm"),
                UserId = usuario.UserId,
            };

            if (usuario.ActualizadoEl != null)
            {
                model.StringOfActualizadoEl = usuario.ActualizadoEl.Value.ToString("dd/MM/yyyy HH:mm");
            }

            foreach (var role in await _userManager.GetRolesAsync(usuario.User))
            {
                IdentityRole identityRole = await _roleManager.FindByNameAsync(role);
                model.RoleId = identityRole.Id;
            }

            return Json(new { success = true, empleado = model });
        }

        public async Task<IActionResult> GetByServicio(int id)
        {
            Servicio servicio = await _context.Servicio.FindAsync(id); ;
            if (servicio is null)
            {
                return Json(new { success = false, message = "El servicio no se encontró" });
            }

            List<Option> options = new List<Option>();

            foreach (Usuario usuario in await _context.Usuario.Where(u => u.UserId != null).ToListAsync())
            {
                foreach (Modulo modulo in await _context.Modulo.Where(m => m.ServicioId == id && m.Disponible == '1').ToListAsync())
                {
                    if (usuario.Id == modulo.UsuarioId)
                    {
                        options.Add(new Option() { Value = usuario.Id, Text = usuario.Nombre + " " + usuario.ApPaterno + " " + usuario.ApMaterno });
                    }
                }
            }

            return Json(new { success = true, empleados = options });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Empleado empleado)
        {
            if (ModelState.IsValid)
            {

                User user = await _userManager.FindByIdAsync(empleado.UserId);
                Usuario usuario = await _context.Usuario.FindAsync(empleado.Id);

                if (user is null || usuario is null)
                {
                    return Json(new { success = false, message = "El empleado no se encontró" });
                }

                user.Email = empleado.Email;

                IdentityResult identityResult = await _userManager.UpdateAsync(user);

                if (identityResult.Succeeded)
                {

                    IdentityRole identityRole = await _roleManager.FindByIdAsync(empleado.RoleId);
                    string roleName = null;

                    foreach (var role in await _userManager.GetRolesAsync(user))
                    {
                        roleName = role;
                    }

                    if (identityRole.Name != roleName)
                    {
                        await _userManager.RemoveFromRoleAsync(user, roleName);
                        await _userManager.AddToRoleAsync(user, identityRole.Name);
                    }

                    usuario.Nombre = empleado.Nombre;
                    usuario.ApPaterno = empleado.ApPaterno;
                    usuario.ApMaterno = empleado.ApMaterno;
                    usuario.FonoFijo = empleado.FonoFijo;
                    usuario.FonoMovil = empleado.FonoMovil;
                    usuario.GeneroId = empleado.GeneroId;
                    usuario.EstadoCivilId = empleado.EstadoCivilId;
                    usuario.ComunaId = empleado.ComunaId;
                    usuario.Direccion = empleado.Direccion;
                    usuario.FechaNacimiento = empleado.FechaNacimiento;
                    usuario.ActualizadoEl = DateTime.Now;

                    _context.Update(usuario);
                    await _context.SaveChangesAsync();

                    return Json(new { success = true, title = "El empleado se ha modificado!" });
                }

                foreach (var errors in identityResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, errors.Description);
                }
            }
            return Json(new { success = false, errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList(), message = "Se detectó " + ModelState.Values.SelectMany(x => x.Errors).Count() + " error(es)." });
        }

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Usuario usuario = await _context.Usuario.Include(u => u.Modulo).FirstOrDefaultAsync(u => u.Id == id);
            User user = await _userManager.FindByIdAsync(usuario.UserId);

            if (usuario is null || user is null)
            {
                return Json(new { success = false, title = "El empleado no se encontró" });
            }

            if (usuario.Modulo.Count() > 0)
            {
                return Json(new { success = false, title = "Oops...existen registros asociados al empleado" });
            }

            _context.Usuario.Remove(usuario);
            await _context.SaveChangesAsync();
            await _userManager.DeleteAsync(user);

            return Json(new { success = true, title = "El empleado se ha eliminado!" });
        }

    }
}
