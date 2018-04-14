using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetCoreApp.Models;

namespace NetCoreApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly DatabaseContext _context;

        public HomeController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index(Event eventt)
        {
            var listofallevents = (from e in _context.Events
                                   select e).ToList();

            return View(listofallevents);
        }

        [HttpPost]
        public IActionResult Index()
        {
            return View();
        }

        /*[HttpGet]
        public IActionResult Reserve()
        {
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public IActionResult Reserve(Event eventt, ReservationOrBuying reservationOrBuying)
        {
            if (TempData["usernamee1"] != null)
            {
                var updateinfo = (from e in _context.Events
                                  where e.Id == eventt.Id
                                  select e).First();

                updateinfo.RemainingSpace = updateinfo.RemainingSpace - 1;
                _context.SaveChanges();
            }
            TempData.Keep();
            return RedirectToAction("Index","Home");
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

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
