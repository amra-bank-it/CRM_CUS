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
    public class TransactionsController : Controller
    {
        private readonly CustomersContext _context;

        public TransactionsController(CustomersContext context)
        {
            _context = context;
        }

        // GET: Transactions
        public async Task<IActionResult> Index()
        {
            var customersContext = _context.Transactions.Include(t => t.Currency).Include(t => t.Person).Include(t => t.TransactionType);
            return View(await customersContext.ToListAsync());
        }

        // GET: Transactions/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Transactions == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .Include(t => t.Currency)
                .Include(t => t.Person)
                .Include(t => t.TransactionType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // GET: Transactions/Create
        public IActionResult Create()
        {
            ViewData["CurrencyId"] = new SelectList(_context.Isocurrencies, "Id", "NameCurrency");
            ViewData["PersonId"] = new SelectList(_context.People, "Id", "FullName");
            ViewData["TransactionTypeId"] = new SelectList(_context.TransactionTypes, "Id", "Description");
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PersonId,ServerTime,TransactionTypeId,Amount,CurrencyId")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                transaction.Id = Guid.NewGuid();
                _context.Add(transaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CurrencyId"] = new SelectList(_context.Isocurrencies, "Id", "Id", transaction.CurrencyId);
            ViewData["PersonId"] = new SelectList(_context.People, "Id", "Id", transaction.PersonId);
            ViewData["TransactionTypeId"] = new SelectList(_context.TransactionTypes, "Id", "Id", transaction.TransactionTypeId);
            return View(transaction);
        }

        // GET: Transactions/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Transactions == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }
            ViewData["CurrencyId"] = new SelectList(_context.Isocurrencies, "Id", "Id", transaction.CurrencyId);
            ViewData["PersonId"] = new SelectList(_context.People, "Id", "Id", transaction.PersonId);
            ViewData["TransactionTypeId"] = new SelectList(_context.TransactionTypes, "Id", "Id", transaction.TransactionTypeId);
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,PersonId,ServerTime,TransactionTypeId,Amount,CurrencyId")] Transaction transaction)
        {
            if (id != transaction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transaction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactionExists(transaction.Id))
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
            ViewData["CurrencyId"] = new SelectList(_context.Isocurrencies, "Id", "Id", transaction.CurrencyId);
            ViewData["PersonId"] = new SelectList(_context.People, "Id", "Id", transaction.PersonId);
            ViewData["TransactionTypeId"] = new SelectList(_context.TransactionTypes, "Id", "Id", transaction.TransactionTypeId);
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Transactions == null)
            {
                return NotFound();
            }

            var transaction = await _context.Transactions
                .Include(t => t.Currency)
                .Include(t => t.Person)
                .Include(t => t.TransactionType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Transactions == null)
            {
                return Problem("Entity set 'CustomersContext.Transactions'  is null.");
            }
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction != null)
            {
                _context.Transactions.Remove(transaction);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransactionExists(Guid id)
        {
          return (_context.Transactions?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [HttpPost]
        public async Task<IActionResult> Find(Guid id, Transaction transaction, string filterTransaction)
        {
            var dd = _context.Transactions.Where(x => (x.Amount.ToString() + x.Currency.NameCurrency).Contains(filterTransaction)).ToList();

            IEnumerable<Transaction> OutTransaction = dd;
            if (transaction == null)
            {
                return NotFound();
            }
            else if (transaction != null)
            {
                return View("Index", OutTransaction);
            }
            return View("Index", OutTransaction);
        }
    }
}
