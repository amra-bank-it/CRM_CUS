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
    public class ChannelDepositsController : Controller
    {
        private readonly CustomersContext _context;

        public ChannelDepositsController(CustomersContext context)
        {
            _context = context;
        }

        // GET: ChannelDeposits
        public async Task<IActionResult> Index()
        {
            var customersContext = _context.ChannelDeposits.Include(c => c.ChannelLimit);
            return View(await customersContext.ToListAsync());
        }

        // GET: ChannelDeposits/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.ChannelDeposits == null)
            {
                return NotFound();
            }

            var channelDeposit = await _context.ChannelDeposits
                .Include(c => c.ChannelLimit)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (channelDeposit == null)
            {
                return NotFound();
            }

            return View(channelDeposit);
        }

        // GET: ChannelDeposits/Create
        public IActionResult Create()
        {
            ViewData["ChannelLimitId"] = new SelectList(_context.ChannelLimits, "Id", "DescriptionRU");
            return View();
        }

        // POST: ChannelDeposits/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ChannelName,GroupName,DescriptionRu,DescriptionEn,ChannelLimitId")] ChannelDeposit channelDeposit)
        {
            if (ModelState.IsValid)
            {
                channelDeposit.Id = Guid.NewGuid();
                _context.Add(channelDeposit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ChannelLimitId"] = new SelectList(_context.ChannelLimits, "Id", "Id", channelDeposit.ChannelLimitId);
            return View(channelDeposit);
        }

        // GET: ChannelDeposits/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.ChannelDeposits == null)
            {
                return NotFound();
            }

            var channelDeposit = await _context.ChannelDeposits.FindAsync(id);
            if (channelDeposit == null)
            {
                return NotFound();
            }
            ViewData["ChannelLimitId"] = new SelectList(_context.ChannelLimits, "Id", "Id", channelDeposit.ChannelLimitId);
            return View(channelDeposit);
        }

        // POST: ChannelDeposits/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,ChannelName,GroupName,DescriptionRu,DescriptionEn,ChannelLimitId")] ChannelDeposit channelDeposit)
        {
            if (id != channelDeposit.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(channelDeposit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChannelDepositExists(channelDeposit.Id))
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
            ViewData["ChannelLimitId"] = new SelectList(_context.ChannelLimits, "Id", "Id", channelDeposit.ChannelLimitId);
            return View(channelDeposit);
        }

        // GET: ChannelDeposits/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.ChannelDeposits == null)
            {
                return NotFound();
            }

            var channelDeposit = await _context.ChannelDeposits
                .Include(c => c.ChannelLimit)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (channelDeposit == null)
            {
                return NotFound();
            }

            return View(channelDeposit);
        }

        // POST: ChannelDeposits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.ChannelDeposits == null)
            {
                return Problem("Entity set 'CustomersContext.ChannelDeposits'  is null.");
            }
            var channelDeposit = await _context.ChannelDeposits.FindAsync(id);
            if (channelDeposit != null)
            {
                _context.ChannelDeposits.Remove(channelDeposit);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChannelDepositExists(Guid id)
        {
          return (_context.ChannelDeposits?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [HttpPost]
        public async Task<IActionResult> Find(Guid id, ChannelDeposit channelDeposit, string filterChannelDeposit)
        {
            var dd = _context.ChannelDeposits.Where(x => (x.ChannelName + x.GroupName).Contains(filterChannelDeposit)).ToList();

            IEnumerable<ChannelDeposit> OutChannDeposit = dd;
            if (channelDeposit == null)
            {
                return NotFound();
            }
            else if (channelDeposit != null)
            {
                return View("Index", OutChannDeposit);
            }
            return View("Index", OutChannDeposit);
        }
    }
}
