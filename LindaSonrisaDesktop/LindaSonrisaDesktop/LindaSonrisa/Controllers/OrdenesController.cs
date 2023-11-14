using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LindaSonrisa.Data;
using LindaSonrisa.Models.Context;

namespace LindaSonrisa.Controllers
{
    public class OrdenesController : Controller
    {
        private readonly LindaSonrisaContext _context;

        public OrdenesController(LindaSonrisaContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("Ordenes")]
        public async Task<IActionResult> Index()
        {
            ViewData["Ordenes"] = await _context.Orden.ToListAsync();
            ViewData["DetalleOrdenes"] = await _context.DetalleOrden.Include(d => d.Orden).Include(d => d.Producto).ToListAsync();
            return View();
        }

        public class Input
        {
            public string name { get; set; }
            public string value { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] IEnumerable<Input> inputs)
        {
            DateTime dateTimeNow = DateTime.Now;
            Orden orden = new Orden()
            {
                SolicitadoEl = dateTimeNow,
                FueRecepcionada = false
            };

            _context.Add(orden);
            await _context.SaveChangesAsync();

            orden = _context.Orden.Where(o => o.SolicitadoEl == dateTimeNow).FirstOrDefault();

            foreach (Input input in inputs)
            {
                DetalleOrden detalleOrden = new DetalleOrden()
                {
                    ProductoId = int.Parse(input.name),
                    Cantidad = int.Parse(input.value),
                    OrdenId = orden.Id
                };

                _context.Add(detalleOrden);
                await _context.SaveChangesAsync();
            }

            return Json(new { success = true, title = "La órden se ha creado!" });
        }

        // GET: Ordenes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orden = await _context.Orden.FindAsync(id);
            if (orden == null)
            {
                return NotFound();
            }
            return View(orden);
        }

        // POST: Ordenes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SolicitadoEl,ActualizadoEl,Comentario,FueRecepcionada")] Orden orden)
        {
            if (id != orden.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orden);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdenExists(orden.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(orden);
        }

        // GET: Ordenes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orden = await _context.Orden
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orden == null)
            {
                return NotFound();
            }

            return View(orden);
        }

        // POST: Ordenes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orden = await _context.Orden.FindAsync(id);
            _context.Orden.Remove(orden);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdenExists(int id)
        {
            return _context.Orden.Any(e => e.Id == id);
        }
    }
}
