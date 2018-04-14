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
    public class MakeReservationController : Controller
    {
        private readonly DatabaseContext _context;

        public MakeReservationController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: MakeReservation
        public async Task<IActionResult> Index()
        {
            var databaseContext = _context.ReservationsOrBuyings.Include(r => r.Event).Include(r => r.User);
            return View(await databaseContext.ToListAsync());
        }

        
        // GET: MakeReservation/Create
        public IActionResult Create()
        {
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Description");
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email");
            return View();
        }

        
        [HttpPost]
        public IActionResult Create(ReservationOrBuying reservationOrBuying)
        {
            var updateevent = (from e in _context.Events
                               where e.Id == reservationOrBuying.EventId
                               select e).First();
            if (updateevent != null)
            {
                updateevent.RemainingSpace = updateevent.RemainingSpace - 1;
                _context.SaveChanges();
            }
            _context.Add(reservationOrBuying);
            _context.SaveChanges();
                
         
            ViewData["EventId"] = new SelectList(_context.Events, "Id", "Description", reservationOrBuying.EventId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Email", reservationOrBuying.UserId);

            TempData["mess1"] = "Reservation made successfully!";
            return View(reservationOrBuying);
        }
    }
}
