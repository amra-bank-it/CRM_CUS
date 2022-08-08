using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRM_CUS.Models;

namespace CRM_CUS.Controllers
{
    public class IsocurrenciesController : Controller
    {
        private readonly CustomersContext _context;

        public IsocurrenciesController(CustomersContext context)
        {
            _context = context;
        }

        // GET: Isocurrencies
        public async Task<IActionResult> Index()
        {
              return _context.Isocurrencies != null ? 
                          View(await _context.Isocurrencies.ToListAsync()) :
                          Problem("Entity set 'CustomersContext.Isocurrencies'  is null.");
        }

        // GET: Isocurrencies/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Isocurrencies == null)
            {
                return NotFound();
            }

            var isocurrency = await _context.Isocurrencies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (isocurrency == null)
            {
                return NotFound();
            }

            return View(isocurrency);
        }

        // GET: Isocurrencies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Isocurrencies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NameCurrency,CodeCurrency,DescriptionRu")] Isocurrency isocurrency)
        {
            if (ModelState.IsValid)
            {
                isocurrency.Id = Guid.NewGuid();
                _context.Add(isocurrency);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(isocurrency);
        }

        // GET: Isocurrencies/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Isocurrencies == null)
            {
                return NotFound();
            }

            var isocurrency = await _context.Isocurrencies.FindAsync(id);
            if (isocurrency == null)
            {
                return NotFound();
            }
            return View(isocurrency);
        }

        // POST: Isocurrencies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,NameCurrency,CodeCurrency,DescriptionRu")] Isocurrency isocurrency)
        {
            if (id != isocurrency.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(isocurrency);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IsocurrencyExists(isocurrency.Id))
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
            return View(isocurrency);
        }

        // GET: Isocurrencies/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Isocurrencies == null)
            {
                return NotFound();
            }

            var isocurrency = await _context.Isocurrencies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (isocurrency == null)
            {
                return NotFound();
            }

            return View(isocurrency);
        }

        // POST: Isocurrencies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Isocurrencies == null)
            {
                return Problem("Entity set 'CustomersContext.Isocurrencies'  is null.");
            }
            var isocurrency = await _context.Isocurrencies.FindAsync(id);
            if (isocurrency != null)
            {
                _context.Isocurrencies.Remove(isocurrency);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IsocurrencyExists(Guid id)
        {
          return (_context.Isocurrencies?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [HttpPost]
        public async Task<IActionResult> Find(Guid id, Isocurrency isocurrency, string filterIsocurrency)
        {
            var dd = _context.Isocurrencies.Where(x => (x.NameCurrency + x.CodeCurrency).Contains(filterIsocurrency)).ToList();

            IEnumerable<Isocurrency> OutIsocurrency = dd;
            if (isocurrency == null)
            {
                return NotFound();
            }
            else if (isocurrency != null)
            {
                return View("Index", OutIsocurrency);
            }
            return View("Index", OutIsocurrency);
        }
    }
}
