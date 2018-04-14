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
    public class UsersController : Controller
    {
        private readonly DatabaseContext _context;

        public UsersController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .SingleOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(User user)
        {
            bool checkusername = _context.Users.Any(x => x.Username.Equals(user.Username));
            bool checkemail = _context.Users.Any(x => x.Email.Equals(user.Email));
            if (!checkusername && !checkemail)
            {
                _context.Add(user);
                _context.SaveChanges();
                TempData["notice"] = "Successfully registered";
                //return RedirectToAction("Index", "Home");
                return View();
            }
            else
            {
                TempData["notice"] = "Unsuccessfully registered";
                return View();
            }
            
            
        }
    }
}
