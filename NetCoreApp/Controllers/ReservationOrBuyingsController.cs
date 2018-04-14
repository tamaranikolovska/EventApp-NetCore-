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
    public class ReservationOrBuyingsController : Controller
    {
        private readonly DatabaseContext _context;

        public ReservationOrBuyingsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: ReservationOrBuyings
        public async Task<IActionResult> Index()
        {
            var databaseContext = _context.ReservationsOrBuyings.Include(r => r.Event).Include(r => r.User);
            return View(await databaseContext.ToListAsync());
        }

        [HttpGet]
        public IActionResult ShowList()
        {
            var listofreservations = (from r in _context.Events
                                      join rr in _context.ReservationsOrBuyings on r.Id equals rr.EventId
                                      join u in _context.Users on rr.UserId equals u.Id
                                      where u.Username == (String)TempData["username"]
                                      select rr).ToList();
            TempData.Keep();
            return View(listofreservations);
        }

        [HttpPost]
        public IActionResult ShowList(ReservationOrBuying rb)
        {

            return View();
        }
        
    }
}
