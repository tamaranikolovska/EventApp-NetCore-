using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NetCoreApp.Models;

namespace NetCoreApp.Controllers
{
    public class EventsController : Controller
    {
        private readonly DatabaseContext _context;

        public EventsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Events
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewData["AdminId"] = new SelectList(_context.Administrators, "Id", "Email");
            return View();
        }

        [HttpPost]
        public IActionResult Create(Event eventt)
        {
            _context.Add(eventt);
            _context.SaveChanges();
                
            ViewData["AdminId"] = new SelectList(_context.Administrators, "Id", "Email", eventt.AdminId);
            return View(eventt);
        }
    }
}
