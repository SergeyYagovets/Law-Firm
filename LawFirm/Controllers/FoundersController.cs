using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LawFirm.Data;
using LawFirm.Models;

namespace LawFirm.Controllers
{
    public class FoundersController : Controller
    {
        private LawFirmContext _context;

        public FoundersController(LawFirmContext context)
        {
            _context = context;
        }

        // GET: Founders
        public async Task<IActionResult> Index()
        {
            var lawFirmContext = _context.Founders.Include(f => f.Customer);
            return View(await lawFirmContext.ToListAsync());
        }

        // GET: Founders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var founder = await _context.Founders
                .Include(f => f.Customer)
                .FirstOrDefaultAsync(m => m.FounderId == id);
            if (founder == null)
            {
                return NotFound();
            }

            return View(founder);
        }

        // GET: Founders/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "INN");
            return View();
        }

        // POST: Founders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FounderId,INN,FirstName,LastName,Patronymic,CustomerData,CustomerUpdate,CustomerId")] Founder founder)
        {
            founder.CustomerData = DateTime.Now;
            founder.CustomerUpdate = DateTime.Now;
        
            var values = _context.Founders.Where(f => f.CustomerId == founder.CustomerId);
            switch (_context.Customers.Find(founder.CustomerId).Type)
            {
                case "Individual entrepreneur" :
                    if (values.ToArray().Length >= 1)
                    {
                        ViewData["CustomerId"] = new SelectList(_context.Customers.Where(c => c.Id != founder.CustomerId), "Id", "INN");                        
                        return View();
                    }
                    break;
            }

            if (ModelState.IsValid)
            {
                _context.Add(founder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "INN", founder.CustomerId);
            return View(founder);
        }

        // GET: Founders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var founder = await _context.Founders.FindAsync(id);
            if (founder == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "INN", founder.CustomerId);
            return View(founder);
        }

        // POST: Founders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FounderId,INN,FirstName,LastName,Patronymic,CustomerData,CustomerUpdate,CustomerId")] Founder founder)
        {
            if (id != founder.FounderId)
            {
                return NotFound();
            }

            founder.CustomerUpdate = DateTime.Now;


            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(founder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FounderExists(founder.FounderId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "INN", founder.CustomerId);
            return View(founder);
        }

        // GET: Founders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var founder = await _context.Founders
                .Include(f => f.Customer)
                .FirstOrDefaultAsync(m => m.FounderId == id);
            if (founder == null)
            {
                return NotFound();
            }

            return View(founder);
        }

        // POST: Founders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var founder = await _context.Founders.FindAsync(id);
            _context.Founders.Remove(founder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FounderExists(int id)
        {
            return _context.Founders.Any(e => e.FounderId == id);
        }
    }
}
