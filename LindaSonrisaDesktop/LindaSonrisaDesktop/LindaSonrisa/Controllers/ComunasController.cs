using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LindaSonrisa.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LindaSonrisa.Controllers
{
    public class ComunasController : Controller
    {
        private readonly LindaSonrisaContext _context;

        public ComunasController(LindaSonrisaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> GetAll(int id)
        {
            var lindaSonrisaDbContext = _context.Comuna.Where(c => c.RegionId == id).OrderBy(c => c.Titulo);
            return Json(new { data = await lindaSonrisaDbContext.ToListAsync() });
        }
    }
}
