using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LindaSonrisa.Models;
using Microsoft.AspNetCore.Http;

namespace LindaSonrisa.Controllers
{
    public class HomeController : Controller
    {
        private readonly GoogleApiDrive _googleApiDrive;

        public HomeController(GoogleApiDrive googleApiDrive)
        {
            _googleApiDrive = googleApiDrive; 
        }

        public IActionResult Index()
        {
            return View(_googleApiDrive.GetFiles());
        }

        [HttpPost]
        public async Task<ActionResult> UploadFile(IFormFile file)
        {
            await _googleApiDrive.UploadFile(file, "prueba");
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeleteFile(string id)
        {
            _googleApiDrive.DeleteFile(id);
            return RedirectToAction("Index");
        }

        public async Task<FileStreamResult> DownloadFile(string id)
        {
            var file = _googleApiDrive.GetFile(id);
            var content = await _googleApiDrive.DownloadFile(id);
            var type = file.MimeType;
            var name = file.Name;

            return File(content, type, name);
        }

        /*public IActionResult Index()
        {

        }*/

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
