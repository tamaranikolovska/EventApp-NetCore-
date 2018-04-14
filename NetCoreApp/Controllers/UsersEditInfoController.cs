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
    public class UsersEditInfoController : Controller
    {
        private readonly DatabaseContext _context;

        public UsersEditInfoController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: UsersEditInfo
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        [HttpGet]
        public IActionResult Details()
        {
            var getid = (from u in _context.Users
                         where u.Username == (String)TempData["usernamee"]
                         select u.Id).Take(1);
            int finalid = Convert.ToInt32(getid.FirstOrDefault().ToString());

            /*if (getid == null)
            {
                return NotFound();
            }*/

            var user = _context.Users
                .SingleOrDefault(m => m.Id == finalid);
            /*if (user == null)
            {
                return NotFound();
            }*/
            TempData.Keep();
            return View(user);
        }
        
    }
}
