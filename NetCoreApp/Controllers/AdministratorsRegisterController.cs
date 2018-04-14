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
    public class AdministratorsRegisterController : Controller
    {
        private readonly DatabaseContext _context;

        public AdministratorsRegisterController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: AdministratorsRegister
        public IActionResult Index()
        {
            //return View(await _context.Administrators.ToListAsync());
            return View();
        }


        // GET: AdministratorsRegister/Create
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Create(Administrator administrator)
        {
            bool checkusername = _context.Administrators.Any(x => x.Username.Equals(administrator.Username));

            bool checkemail = _context.Administrators.Any(x => x.Email.Equals(administrator.Email));

            string admincode = "A1234589Aa";

            if ((!checkusername) && (!checkemail))
            {
                if (administrator.RegisterAdminCode.Equals(admincode))
                {
                    _context.Add(administrator);
                    _context.SaveChanges();
                    TempData["notice11"] = "Successfully registered";
                    return View();
                }
                else
                {
                    TempData["notice11"] = "Registration code is incorect";
                    return View();
                }
            }
            else
            {
                TempData["notice11"] = "Username or Email already exist";
                return View();
            }
        }
    }
}
