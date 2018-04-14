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
    public class MenageEventsController : Controller
    {
        private readonly DatabaseContext _context;

        public MenageEventsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: MenageEvents
        public IActionResult Index()
        {
            return View();
        }

        // GET: MenageEvents/Details/5
        /*public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .Include(@ => @.Administrator)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }*/
        [HttpGet]
        public IActionResult ShowAllEvents()
        {
            var listofevents = (from a in _context.Administrators
                              join e in _context.Events on a.Id equals e.AdminId
                              where a.Username == (String)TempData["adusername1"]
                              select e).ToList();

            /*int adminid = Convert.ToInt32(getamdinid.FirstOrDefault().ToString());

            var listofevents = (from e in _context.Events
                                where e.AdminId == adminid
                                select e).ToList();*/
            TempData.Keep();
            return View(listofevents);
        }

        [HttpPost]
        public IActionResult ShowAllEvents(Event eventt)
        {
            return View();
        }

        // GET: MenageEvents/Edit/5
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = _context.Events.SingleOrDefault(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }
            ViewData["AdminId"] = new SelectList(_context.Administrators, "Id", "Email", @event.AdminId);
            return View(@event);
        }

        [HttpPost]
        public IActionResult Edit(int id, [Bind("Id,Datefrom,DateTo,Description,TotalSpace,RemainingSpace,Takesplace,Price,AdminId")] Event @event)
        {
            _context.Update(@event);
            _context.SaveChanges();
                
            //return RedirectToAction(nameof(Index));
            
            //ViewData["AdminId"] = new SelectList(_context.Administrators, "Id", "Email", @event.AdminId);
            return View(@event);
        }

        // GET: MenageEvents/Delete/5
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = _context.Events
                .AsNoTracking()
                .SingleOrDefault(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: MenageEvents/Delete/5
        [HttpPost, ActionName("Delete")]
        public IActionResult Delete(int id)
        {
            var @event =  _context.Events.AsNoTracking().SingleOrDefault(m => m.Id == id);
            _context.Events.Remove(@event);
            _context.SaveChanges();
            return RedirectToAction("ShowAllEvents", "MenageEvents");
        }
    }
}
