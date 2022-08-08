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
    public class AccountDepositsController : Controller
    {
        private readonly CustomersContext _context;

        public AccountDepositsController(CustomersContext context)
        {
            _context = context;
        }

        // GET: AccountDeposits
        public async Task<IActionResult> Index()
        {
            var customersContext = _context.AccountDeposits.Include(a => a.ChannelDeposit).Include(a => a.Person);
            return View(await customersContext.ToListAsync());
        }

        // GET: AccountDeposits/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.AccountDeposits == null)
            {
                return NotFound();
            }

            var accountDeposit = await _context.AccountDeposits
                .Include(a => a.ChannelDeposit)
                .Include(a => a.Person)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accountDeposit == null)
            {
                return NotFound();
            }

            return View(accountDeposit);
        }

        // GET: AccountDeposits/Create
        public IActionResult Create()
        {
            ViewData["ChannelDepositId"] = new SelectList(_context.ChannelDeposits, "Id", "Id");
            ViewData["PersonId"] = new SelectList(_context.People, "Id", "Id");
            return View();
        }

        // POST: AccountDeposits/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PersonId,Account,AccountMasked,Bank,ChannelDepositId,Available")] AccountDeposit accountDeposit)
        {
            if (ModelState.IsValid)
            {
                accountDeposit.Id = Guid.NewGuid();
                _context.Add(accountDeposit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ChannelDepositId"] = new SelectList(_context.ChannelDeposits, "Id", "Id", accountDeposit.ChannelDepositId);
            ViewData["PersonId"] = new SelectList(_context.People, "Id", "Id", accountDeposit.PersonId);
            return View(accountDeposit);
        }

        // GET: AccountDeposits/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.AccountDeposits == null)
            {
                return NotFound();
            }

            var accountDeposit = await _context.AccountDeposits.FindAsync(id);
            if (accountDeposit == null)
            {
                return NotFound();
            }
            ViewData["ChannelDepositId"] = new SelectList(_context.ChannelDeposits, "Id", "Id", accountDeposit.ChannelDepositId);
            ViewData["PersonId"] = new SelectList(_context.People, "Id", "Id", accountDeposit.PersonId);
            return View(accountDeposit);
        }

        // POST: AccountDeposits/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,PersonId,Account,AccountMasked,Bank,ChannelDepositId,Available")] AccountDeposit accountDeposit)
        {
            if (id != accountDeposit.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accountDeposit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountDepositExists(accountDeposit.Id))
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
            ViewData["ChannelDepositId"] = new SelectList(_context.ChannelDeposits, "Id", "Id", accountDeposit.ChannelDepositId);
            ViewData["PersonId"] = new SelectList(_context.People, "Id", "Id", accountDeposit.PersonId);
            return View(accountDeposit);
        }

        // GET: AccountDeposits/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.AccountDeposits == null)
            {
                return NotFound();
            }

            var accountDeposit = await _context.AccountDeposits
                .Include(a => a.ChannelDeposit)
                .Include(a => a.Person)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (accountDeposit == null)
            {
                return NotFound();
            }

            return View(accountDeposit);
        }

        // POST: AccountDeposits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.AccountDeposits == null)
            {
                return Problem("Entity set 'CustomersContext.AccountDeposits'  is null.");
            }
            var accountDeposit = await _context.AccountDeposits.FindAsync(id);
            if (accountDeposit != null)
            {
                _context.AccountDeposits.Remove(accountDeposit);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountDepositExists(Guid id)
        {
          return (_context.AccountDeposits?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [HttpPost]
        public async Task<IActionResult> Find(Guid id, AddressType addressType, string filterAccDeposit)
        {
            var dd = _context.AccountDeposits.Where(x => (x.Account + x.Person.FullName + x.Bank).Contains(filterAccDeposit)).ToList();

            IEnumerable<AccountDeposit> OutAccDeposit = dd;
            if (addressType == null)
            {
                return NotFound();
            }
            else if (addressType != null)
            {
                return View("Index", OutAccDeposit);
            }
            //return View(OutPers);
            return View("Index", OutAccDeposit);
        }
    }
}
