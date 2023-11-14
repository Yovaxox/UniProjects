using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LindaSonrisa.Data;
using LindaSonrisa.Models.Identity;
using LindaSonrisa.Models.Views;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LindaSonrisa.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly LindaSonrisaContext _context;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;

        public AccountController(LindaSonrisaContext context, SignInManager<User> signInManager, UserManager<User> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return View("Index");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(Login login)
        {
            /*if (!ValidarRut(login.Rut))
            {
                ModelState.AddModelError(string.Empty, "El RUT ingresado no es válido.");
                return Json(new { success = false, errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList(), message = "Se detectó " + ModelState.Values.SelectMany(x => x.Errors).Count() + " error(es)." });
            }*/

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(login.Rut, login.Password, isPersistent: false, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    User user = await _userManager.FindByNameAsync(login.Rut);
                    //Usuario usuario = await _context.Usuario.Include(u => u.User).Where(u => u.AspNetUserId == user.Id).FirstOrDefaultAsync();

                    //string[] nombres = usuario.Nombre.Split(" ");

                    return Json(new { success = true, title = "Bienvenido" + /*nombres[0] +*/ "!", action = Url.Action("Index", "Account") });
                }

                ModelState.AddModelError(string.Empty, "Tus credenciales no coinciden con nuestros registros.");
            }

            return Json(new { success = false, errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList(), message = "Se detectó " + ModelState.Values.SelectMany(x => x.Errors).Count() + " error(es)." });
        }

        public static bool ValidarRut(string rut)
        {
            rut = rut.Replace(".", "").ToUpper();
            Regex expresion = new Regex("^([0-9]+-[0-9K])$");
            string dv = rut.Substring(rut.Length - 1, 1);
            if (!expresion.IsMatch(rut))
            {
                return false;
            }
            char[] charCorte = { '-' };
            string[] rutTemp = rut.Split(charCorte);
            if (dv != Digito(int.Parse(rutTemp[0])))
            {
                return false;
            }
            return true;
        }

        public static string Digito(int rut)
        {
            int suma = 0;
            int multiplicador = 1;
            while (rut != 0)
            {
                multiplicador++;
                if (multiplicador == 8)
                    multiplicador = 2;
                suma += (rut % 10) * multiplicador;
                rut = rut / 10;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
