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
    public class UsersLoginController : Controller
    {
        private readonly DatabaseContext _context;

        public UsersLoginController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: UsersLogin
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        // GET: UsersLogin/Create
        public IActionResult LogIn()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult LogIn(User user)
        {
            var checkiflogedin = (from u in _context.Users
                                  where u.Username.Equals(user.Username) &&
                                  u.Password.Equals(user.Password)
                                  select u).FirstOrDefault();

            if (checkiflogedin != null)
            {
                TempData["username"] = user.Username;
                TempData.Keep();
                TempData["usernamee"] = user.Username;
                TempData.Keep();
                TempData["usernamee1"] = user.Username;
                TempData.Keep();
                TempData["usernamee11"] = user.Username;
                TempData.Keep();
                //TempData["notice1"] = "Successfully loged in";
                return RedirectToAction("Details", "UsersEditInfo");
                //return UserNameInterface();
            }
            else
            {
                TempData["notice1"] = "Your credentials are incorrect";
                return View();
            }
        }

        public IActionResult LogOut()
        {
            TempData["username"] = null;
            TempData["usernamee"] = null;
            TempData["usernamee1"] = null;
            TempData["usernamee11"] = null;
            return RedirectToAction("LogIn", "UsersLogin");
        }
    }
}
