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
    public class ChannelLimitsController : Controller
    {
        private readonly CustomersContext _context;

        public ChannelLimitsController(CustomersContext context)
        {
            _context = context;
        }

        // GET: ChannelLimits
        public async Task<IActionResult> Index()
        {
            var customersContext = _context.ChannelLimits.Include(c => c.TransactionType);
            return View(await customersContext.ToListAsync());
        }

        // GET: ChannelLimits/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.ChannelLimits == null)
            {
                return NotFound();
            }

            var channelLimit = await _context.ChannelLimits
                .Include(c => c.TransactionType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (channelLimit == null)
            {
                return NotFound();
            }

            return View(channelLimit);
        }

        // GET: ChannelLimits/Create
        public IActionResult Create()
        {
            ViewData["TransactionTypeId"] = new SelectList(_context.TransactionTypes, "Id", "Description");
            return View();
        }

        // POST: ChannelLimits/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TrnDailyAmount,TrnMonthlyAmount,TrnDailyCount,TrnMonthlyCount,DescriptionRu,TransactionTypeId")] ChannelLimit channelLimit)
        {
            if (ModelState.IsValid)
            {
                channelLimit.Id = Guid.NewGuid();
                _context.Add(channelLimit);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TransactionTypeId"] = new SelectList(_context.TransactionTypes, "Id", "Id", channelLimit.TransactionTypeId);
            return View(channelLimit);
        }

        // GET: ChannelLimits/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.ChannelLimits == null)
            {
                return NotFound();
            }

            var channelLimit = await _context.ChannelLimits.FindAsync(id);
            if (channelLimit == null)
            {
                return NotFound();
            }
            ViewData["TransactionTypeId"] = new SelectList(_context.TransactionTypes, "Id", "Id", channelLimit.TransactionTypeId);
            return View(channelLimit);
        }

        // POST: ChannelLimits/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,TrnDailyAmount,TrnMonthlyAmount,TrnDailyCount,TrnMonthlyCount,DescriptionRu,TransactionTypeId")] ChannelLimit channelLimit)
        {
            if (id != channelLimit.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(channelLimit);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChannelLimitExists(channelLimit.Id))
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
            ViewData["TransactionTypeId"] = new SelectList(_context.TransactionTypes, "Id", "Id", channelLimit.TransactionTypeId);
            return View(channelLimit);
        }

        // GET: ChannelLimits/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.ChannelLimits == null)
            {
                return NotFound();
            }

            var channelLimit = await _context.ChannelLimits
                .Include(c => c.TransactionType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (channelLimit == null)
            {
                return NotFound();
            }

            return View(channelLimit);
        }

        // POST: ChannelLimits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.ChannelLimits == null)
            {
                return Problem("Entity set 'CustomersContext.ChannelLimits'  is null.");
            }
            var channelLimit = await _context.ChannelLimits.FindAsync(id);
            if (channelLimit != null)
            {
                _context.ChannelLimits.Remove(channelLimit);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChannelLimitExists(Guid id)
        {
          return (_context.ChannelLimits?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [HttpPost]
        public async Task<IActionResult> Find(Guid id, ChannelLimit channelLimit, string filterChannelLimit)
        {
            var dd = _context.ChannelLimits.Where(x => x.TrnDailyCount.ToString().Contains(filterChannelLimit)).ToList();

            IEnumerable<ChannelLimit> OutChannLimit = dd;
            if (channelLimit == null)
            {
                return NotFound();
            }
            else if (channelLimit != null)
            {
                return View("Index", OutChannLimit);
            }
            return View("Index", OutChannLimit);
        }
    }
}
