using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LindaSonrisa.Data;
using LindaSonrisa.Models;
using LindaSonrisa.Models.Views;
using Microsoft.AspNetCore.Http;
using Google.Apis.Drive.v3.Data;
using Servicio = LindaSonrisa.Models.Context.Servicio;

namespace LindaSonrisa.Controllers
{
    public class ServiciosController : Controller
    {
        private readonly LindaSonrisaContext _context;
        private readonly GoogleApiDrive _googleApiDrive;

        public ServiciosController(LindaSonrisaContext context, GoogleApiDrive googleApiDrive)
        {
            _context = context;
            _googleApiDrive = googleApiDrive;
        }

        [HttpGet]
        [Route("Servicios")]
        public async Task<IActionResult> Index()
        {
            ViewData["NotFoundImage"] = "~/images/internet.svg";
            ViewData["Servicios"] = await _context.Servicio.ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Models.Views.Servicio model)
        {
            if (ModelState.IsValid)
            {

                File folder = _googleApiDrive.GetFolder("Servicios");
                if (folder == null)
                {
                    folder = _googleApiDrive.CreateFolder("Servicios");
                }

                if (await _context.Servicio.FirstOrDefaultAsync(s => s.TituloNormalizado == model.NombreNormalizado) != null)
                {
                    return Json(new { success = false, errors = new List<string>() { "El servicio (" + model.Nombre + ") se encuentra registrado." }, message = "Se detectó 1 error." });
                }
                else
                {
                    Servicio servicio = new Servicio()
                    {
                        Titulo = model.Nombre,
                        TituloNormalizado = model.NombreNormalizado,
                        Precio = model.Precio,
                        Descuento = model.Descuento,
                        Descripcion = model.Descripcion,
                        Comentario = model.Comentario,
                        EsPromocion = model.EsPromocion == true ? '1' : '0',
                        EstaInactivo = model.EstaInactivo == true ? '1' : '0',
                    };
                    _context.Add(servicio);
                    await _context.SaveChangesAsync();

                    servicio = await _context.Servicio.FirstOrDefaultAsync(s => s.TituloNormalizado == model.NombreNormalizado);

                    if (model.Image != null)
                    {
                        File file = await _googleApiDrive.UploadFileInFolder(model.Image, servicio.Id.ToString(), folder.Id);
                        servicio.FileId = file.Id;
                        _context.Update(servicio);
                        await _context.SaveChangesAsync();
                    }

                    return Json(new { success = true, title = "El servicio se ha creado!" });
                }
            }
            return Json(new { success = false, errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList(), message = "Se detectó " + ModelState.Values.SelectMany(x => x.Errors).Count() + " error(es)." });
        }

        [HttpGet]
        public async Task<IActionResult> Get(int? id)
        {
            Servicio servicio = await _context.Servicio.FindAsync(id);
            if (servicio == null)
            {
                return Json(new { success = false, message = "El servicio no se encontró" });
            }

            Models.Views.Servicio model = new Models.Views.Servicio();

            model.Id = servicio.Id;
            model.Nombre = servicio.Titulo;
            model.Precio = servicio.Precio;
            model.Descuento = servicio.Descuento;
            model.Descripcion = servicio.Descripcion;
            model.Comentario = servicio.Comentario;
            model.EsPromocion = servicio.EsPromocion == '1' ? true : false;
            model.EstaInactivo = servicio.EstaInactivo == '1' ? true : false;
            
            if (servicio.FileId != null)
            {
                model.HasImage = true;
            }
            return Json(new { success = true, servicio = model });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Models.Views.Servicio model)
        {
            if (ModelState.IsValid)
            {

                Servicio servicio = await _context.Servicio.FindAsync(model.Id);

                if (servicio is null)
                {
                    return Json(new { success = true, errors = new List<string>() { "No se encontró el servicio." }, message = "Se detectó 1 error." });
                }

                File folder = _googleApiDrive.GetFolder("Servicios");
                if (folder is null)
                {
                    folder = _googleApiDrive.CreateFolder("Servicios");
                }

                if (servicio.TituloNormalizado != model.NombreNormalizado)
                {
                    if (await _context.Servicio.FirstOrDefaultAsync(s => s.TituloNormalizado == model.NombreNormalizado) != null)
                    {
                        return Json(new { success = false, errors = new List<string>() { "El servicio (" + model.Nombre + ") se encuentra registrado." }, message = "Se detectó 1 error." });
                    }
                }

                servicio.Titulo = model.Nombre;
                servicio.TituloNormalizado = model.NombreNormalizado;
                servicio.Precio = model.Precio;
                servicio.Descuento = model.Descuento;
                servicio.Descripcion = model.Descripcion;
                servicio.Comentario = model.Comentario;
                servicio.EsPromocion = model.EsPromocion == true ? '1' : '0';

                if (model.Image != null)
                {
                    if (servicio.FileId != null)
                    {
                        _googleApiDrive.DeleteFile(servicio.FileId);
                    }
                    File file = await _googleApiDrive.UploadFileInFolder(model.Image, servicio.Id.ToString(), folder.Id);
                    servicio.FileId = file.Id;
                }
                else
                {
                    if (!model.HasImage)
                    {
                        if (servicio.FileId != null)
                        {
                            _googleApiDrive.DeleteFile(servicio.FileId);
                            servicio.FileId = null;
                        }
                    }
                }
                _context.Update(servicio);
                await _context.SaveChangesAsync();
                return Json(new { success = true, title = "El servicio se ha modificado!" });

            }
            return Json(new { success = false, errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList(), message = "Se detectó " + ModelState.Values.SelectMany(x => x.Errors).Count() + " error(es)." });
        }

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Servicio servicio = await _context.Servicio.Include(s => s.Modulo).FirstOrDefaultAsync(s => s.Id == id);
            if (servicio == null)
            {
                return Json(new { success = false, title = "Oops...no se encontró el servicio" });
            }

            if (servicio.Modulo.Count() > 0)
            {
                return Json(new { success = false, title = "Oops...existen registros asociados al servicio" });
            }

            if (servicio.FileId != null)
            {
                _googleApiDrive.DeleteFile(servicio.FileId);
            }

            _context.Servicio.Remove(servicio);
            await _context.SaveChangesAsync();
            return Json(new { success = true, title = "El servicio se ha eliminado!" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeStatus(int id)
        {
            Servicio servicio = await _context.Servicio.FindAsync(id);
            if (servicio == null)
            {
                return Json(new { success = false, title = "Oops...no se encontró el servicio" });
            }

            if (servicio.EstaInactivo == '1')
            {
                servicio.EstaInactivo = '0';
            }
            else
            {
                servicio.EstaInactivo = '1';
            }

            _context.Update(servicio);
            await _context.SaveChangesAsync();
            return Json(new { success = true, title = "El servicio ha cambiado de estado!" });
        }
    }
}
