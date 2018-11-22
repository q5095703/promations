using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Promations.Data;
using Promations.Models;

namespace Promations.Controllers
{
    public class HomeController : Controller
    {

        private readonly DBContext _context;

        public HomeController(DBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public JsonResult Products()
        {
            var dBContext = _context.Products.Where(p => p.IsDeleted == false);
            return Json(dBContext.ToList());
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
