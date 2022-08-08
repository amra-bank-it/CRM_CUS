using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRM_CUS.Models;
using System.Data.Entity.SqlServer;
namespace CRM_CUS.Controllers
{
    public class PassportsController : Controller
    {
        private readonly CustomersContext _context;

        public PassportsController(CustomersContext context)
        {
            _context = context;
        }

        // GET: Passports
        public async Task<IActionResult> Index()
        {
            var customersContext = _context.Passports.Include(p => p.Person).Include(p => p.TypeDocument);

            return View(await customersContext.ToListAsync());
        }

        // GET: Passports/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Passports == null)
            {
                return NotFound();
            }

            var passport = await _context.Passports
                .Include(p => p.Person)
                .Include(p => p.TypeDocument)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (passport == null)
            {
                return NotFound();
            }

            return View(passport);
        }

        // GET: Passports/Create
        public IActionResult Create()
        {
            ViewData["PersonId"] = new SelectList(_context.People, "Id", "FullName");
            ViewData["TypeDocumentId"] = new SelectList(_context.TypeDocuments, "Id", "DescriptionRu");
            return View();
        }

        // POST: Passports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PersonId,TypeDocumentId,Serial,Number,IdDate,IdWhom,IdWhomCode")] Passport passport)
        {
            if (ModelState.IsValid)
            {
                passport.Id = Guid.NewGuid();
                _context.Add(passport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PersonId"] = new SelectList(_context.People, "Id", "FullName", passport.PersonId);
            ViewData["TypeDocumentId"] = new SelectList(_context.TypeDocuments, "Id", "DescriptionRu", passport.TypeDocumentId);
            return View(passport);
        }

        // GET: Passports/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Passports == null)
            {
                return NotFound();
            }

            var passport = await _context.Passports.FindAsync(id);
            if (passport == null)
            {
                return NotFound();
            }
            ViewData["PersonId"] = new SelectList(_context.People, "Id", "FullName", passport.PersonId);
            ViewData["TypeDocumentId"] = new SelectList(_context.TypeDocuments, "Id", "DescriptionRu", passport.TypeDocumentId);
            return View(passport);
        }

        // POST: Passports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,PersonId,TypeDocumentId,Serial,Number,IdDate,IdWhom,IdWhomCode")] Passport passport)
        {
            if (id != passport.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(passport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PassportExists(passport.Id))
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
            ViewData["PersonId"] = new SelectList(_context.People, "Id", "FullName", passport.PersonId);
            ViewData["TypeDocumentId"] = new SelectList(_context.TypeDocuments, "Id", "DescriptionRu", passport.TypeDocumentId);
            return View(passport);
        }

        // GET: Passports/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Passports == null)
            {
                return NotFound();
            }

            var passport = await _context.Passports
                .Include(p => p.Person)
                .Include(p => p.TypeDocument)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (passport == null)
            {
                return NotFound();
            }

            return View(passport);
        }

        // POST: Passports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Passports == null)
            {
                return Problem("Entity set 'CustomersContext.Passports'  is null.");
            }
            var passport = await _context.Passports.FindAsync(id);
            if (passport != null)
            {
                _context.Passports.Remove(passport);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PassportExists(Guid id)
        {
          return (_context.Passports?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [HttpPost]
        public async Task<IActionResult> Find(Guid id, Passport passport, string filterPassport)
        {
            var dd = _context.Passports.Where(x => (x.Serial + x.Number).Contains(filterPassport)).ToList();

            IEnumerable<Passport> OutPass = dd;
            if (passport == null)
            {
                return NotFound();
            }
            else if (passport != null)
            {
                return View("Index", OutPass);
            }
            return View("Index", OutPass);
        }
    }
}
