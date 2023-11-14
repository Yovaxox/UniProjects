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
using Producto = LindaSonrisa.Models.Context.Producto;

namespace LindaSonrisa.Controllers
{
    public class ProductosController : Controller
    {
        private readonly LindaSonrisaContext _context;
        private readonly GoogleApiDrive _googleApiDrive;

        public ProductosController(LindaSonrisaContext context, GoogleApiDrive googleApiDrive)
        {
            _context = context;
            _googleApiDrive = googleApiDrive;
        }

        [HttpGet]
        [Route("Productos")]
        public async Task<IActionResult> Index()
        {
            ViewData["NotFoundImage"] = "~/images/internet.svg";
            ViewData["Productos"] = await _context.Producto.Include(p => p.Proveedor).Include(p => p.TipoProducto).ToListAsync();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Models.Views.Producto model)
        {
            if (ModelState.IsValid)
            {

                File folder = _googleApiDrive.GetFolder("Productos");
                if (folder == null)
                {
                    folder = _googleApiDrive.CreateFolder("Productos");
                }

                if (await _context.Producto.FirstOrDefaultAsync(p => p.TituloNormalizado == model.NombreNormalizado) != null)
                {
                    return Json(new { success = false, errors = new List<string>() { "El producto (" + model.Nombre + ") se encuentra registrado." }, message = "Se detectó 1 error." });
                }
                else
                {
                    Producto producto = new Producto()
                    {
                        Titulo = model.Nombre,
                        TituloNormalizado = model.NombreNormalizado.Normalize(),
                        Descuento = model.Descuento,
                        Descripcion = model.Descripcion,
                        Comentario = model.Comentario,
                        EsPromocion = model.EsPromocion == true ? '1' : '0',
                        EstaInactivo = model.EstaInactivo,
                        StockCritico = model.StockCritico,
                        ProveedorId = model.ProveedorId,
                        TipoProductoId = model.TipoProductoId
                    };
                    _context.Add(producto);
                    await _context.SaveChangesAsync();

                    producto = await _context.Producto.FirstOrDefaultAsync(s => s.TituloNormalizado == model.NombreNormalizado);

                    if (model.Image != null)
                    {
                        File file = await _googleApiDrive.UploadFileInFolder(model.Image, producto.Id.ToString(), folder.Id);
                        producto.FileId = file.Id;
                        _context.Update(producto);
                        await _context.SaveChangesAsync();
                    }

                    return Json(new { success = true, title = "El producto se ha creado!" });
                }
            }
            return Json(new { success = false, errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList(), message = "Se detectó " + ModelState.Values.SelectMany(x => x.Errors).Count() + " error(es)." });
        }

        [HttpGet]
        public async Task<IActionResult> Get(int? id)
        {
            Producto producto = await _context.Producto.FindAsync(id);
            if (producto == null)
            {
                return Json(new { success = false, message = "El producto no se encontró" });
            }

            Models.Views.Producto model = new Models.Views.Producto();

            model.Id = producto.Id;
            model.Nombre = producto.Titulo;
            model.Descuento = producto.Descuento;
            model.Descripcion = producto.Descripcion;
            model.Comentario = producto.Comentario;
            model.EsPromocion = producto.EsPromocion == '1' ? true : false;
            model.EstaInactivo = producto.EstaInactivo;
            model.StockCritico = producto.StockCritico;
            model.ProveedorId = producto.ProveedorId;
            model.TipoProductoId = producto.TipoProductoId;


            if (producto.FileId != null)
            {
                model.HasImage = true;
            }

            return Json(new { success = true, producto = model });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllByProveedor(int id)
        {
            List<Producto> productos = await _context.Producto.Where(p => p.ProveedorId == id && p.EstaInactivo == false).OrderBy(p => p.Titulo).ToListAsync();

            if (productos.Count() == 0)
            {
                return Json(new { success = false, message = "No se encontró productos" });
            }

            return Json(new { success = true, productos = productos });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Models.Views.Producto model)
        {
            if (ModelState.IsValid)
            {

                File folder = _googleApiDrive.GetFolder("Productos");
                if (folder is null)
                {
                    folder = _googleApiDrive.CreateFolder("Productos");
                }

                Producto producto = await _context.Producto.FindAsync(model.Id);

                if (producto is null)
                {
                    return Json(new { success = true, errors = new List<string>() { "No se encontró el producto." }, message = "Se detectó 1 error." });
                }

                if (producto.TituloNormalizado != model.NombreNormalizado)
                {
                    if (await _context.Producto.FirstOrDefaultAsync(p => p.TituloNormalizado == model.NombreNormalizado) != null)
                    {
                        return Json(new { success = false, errors = new List<string>() { "El producto (" + model.Nombre + ") se encuentra registrado." }, message = "Se detectó 1 error." });
                    }
                }

                producto.Titulo = model.Nombre;
                producto.TituloNormalizado = model.NombreNormalizado;
                producto.Descuento = model.Descuento;
                producto.Descripcion = model.Descripcion;
                producto.Comentario = model.Comentario;
                producto.EsPromocion = model.EsPromocion == true ? '1' : '0';
                producto.StockCritico = model.StockCritico;
                producto.ProveedorId = model.ProveedorId;
                producto.TipoProductoId = model.TipoProductoId;

                if (model.Image != null)
                {
                    if (producto.FileId != null)
                    {
                        _googleApiDrive.DeleteFile(producto.FileId);
                    }
                    File file = await _googleApiDrive.UploadFileInFolder(model.Image, producto.Id.ToString(), folder.Id);
                    producto.FileId = file.Id;
                }
                else
                {
                    if (!model.HasImage)
                    {
                        if (producto.FileId != null)
                        {
                            _googleApiDrive.DeleteFile(producto.FileId);
                            producto.FileId = null;
                        }
                    }
                }
                _context.Update(producto);
                await _context.SaveChangesAsync();
                return Json(new { success = true, title = "El producto se ha modificado!" });

            }
            return Json(new { success = false, errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList(), message = "Se detectó " + ModelState.Values.SelectMany(x => x.Errors).Count() + " error(es)." });
        }

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            Producto producto = await _context.Producto.Include(p => p.DetalleOrden).FirstOrDefaultAsync(s => s.Id == id);

            if (producto == null)
            {
                return Json(new { success = false, title = "Oops...no se encontró el producto" });
            }

            if (producto.DetalleOrden.Count() > 0)
            {
                return Json(new { success = false, title = "Oops...existen registros asociados al producto" });
            }

            if (producto.FileId != null)
            {
                _googleApiDrive.DeleteFile(producto.FileId);
            }

            _context.Producto.Remove(producto);
            await _context.SaveChangesAsync();
            return Json(new { success = true, title = "El producto se ha eliminado!" });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeStatus(int id)
        {
            Producto producto = await _context.Producto.FindAsync(id);
            if (producto == null)
            {
                return Json(new { success = false, title = "Oops...no se encontró el producto" });
            }

            if (!producto.EstaInactivo)
            {
                producto.EstaInactivo = true;
            }
            else
            {
                producto.EstaInactivo = false;
            }

            _context.Update(producto);
            await _context.SaveChangesAsync();
            return Json(new { success = true, title = "El producto ha cambiado de estado!" });
        }
    }
}
