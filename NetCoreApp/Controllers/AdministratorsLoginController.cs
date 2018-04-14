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
    public class AdministratorsLoginController : Controller
    {
        private readonly DatabaseContext _context;

        public AdministratorsLoginController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: AdministratorsLogin
        public async Task<IActionResult> Index()
        {
            return View(await _context.Administrators.ToListAsync());
        }

        [HttpGet]
        public IActionResult LoginAdmin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LoginAdmin(Administrator administrator)
        {
            var checkiflogedin = (from a in _context.Administrators
                                  where a.Username.Equals(administrator.Username) &&
                                  a.Password.Equals(administrator.Password)
                                  select a).FirstOrDefault();
            if (checkiflogedin != null)
            {
                TempData["adusername"] = administrator.Username;
                TempData.Keep();
                TempData["adusernamee"] = administrator.Username;
                TempData.Keep();
                TempData["adusername1"] = administrator.Username;
                TempData.Keep();
                //TempData["notice1"] = "Successfully loged in";
                return RedirectToAction("Create", "Events");
                //return UserNameInterface();
            }
            else
            {
                TempData["notice1"] = "Your credentials (Username or E-mail) are incorrect";
                return View();
            }
        }

        public IActionResult LogOutAdmin()
        {
            TempData["adusername"] = null;
            TempData["adusernamee"] = null;
            return RedirectToAction("LogIn", "AdministratorsLogin");
        }
    }
}
