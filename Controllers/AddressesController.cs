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
    public class AddressesController : Controller
    {
        private readonly CustomersContext _context;

        public AddressesController(CustomersContext context)
        {
            _context = context;
        }

        // GET: Addresses
        public async Task<IActionResult> Index()
        {
            var customersContext = _context.Addresses.Include(a => a.AddressType).Include(a => a.Person);
            return View(await customersContext.ToListAsync());
        }

        // GET: Addresses/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Addresses == null)
            {
                return NotFound();
            }

            var address = await _context.Addresses
                .Include(a => a.AddressType)
                .Include(a => a.Person)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (address == null)
            {
                return NotFound();
            }

            return View(address);
        }

        // GET: Addresses/Create
        public IActionResult Create()
        {
            ViewData["AddressTypeId"] = new SelectList(_context.AddressTypes, "Id", "Description");
            ViewData["PersonId"] = new SelectList(_context.People, "Id", "FullName");
            return View();
        }

        // POST: Addresses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PersonId,Country,Region,City,Street,House,Building,Apartment,AddressTypeId")] Address address)
        {
            if (ModelState.IsValid)
            {
                address.Id = Guid.NewGuid();
                _context.Add(address);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AddressTypeId"] = new SelectList(_context.AddressTypes, "Id", "Description", address.AddressTypeId);
            ViewData["PersonId"] = new SelectList(_context.People, "Id", "FullName", address.PersonId);
            return View(address);
        }

        // GET: Addresses/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Addresses == null)
            {
                return NotFound();
            }

            var address = await _context.Addresses.FindAsync(id);
            if (address == null)
            {
                return NotFound();
            }
            ViewData["AddressTypeId"] = new SelectList(_context.AddressTypes, "Id", "Description", address.AddressTypeId);
            ViewData["PersonId"] = new SelectList(_context.People, "Id", "FullName", address.PersonId);
            return View(address);
        }

        // POST: Addresses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,PersonId,Country,Region,City,Street,House,Building,Apartment,AddressTypeId")] Address address)
        {
            if (id != address.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(address);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AddressExists(address.Id))
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
            ViewData["AddressTypeId"] = new SelectList(_context.AddressTypes, "Id", "Description", address.AddressTypeId);
            ViewData["PersonId"] = new SelectList(_context.People, "Id", "FullName", address.PersonId);
            return View(address);
        }

        // GET: Addresses/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.Addresses == null)
            {
                return NotFound();
            }

            var address = await _context.Addresses
                .Include(a => a.AddressType)
                .Include(a => a.Person)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (address == null)
            {
                return NotFound();
            }

            return View(address);
        }

        // POST: Addresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Addresses == null)
            {
                return Problem("Entity set 'CustomersContext.Addresses'  is null.");
            }
            var address = await _context.Addresses.FindAsync(id);
            if (address != null)
            {
                _context.Addresses.Remove(address);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AddressExists(Guid id)
        {
          return (_context.Addresses?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [HttpPost]
        public async Task<IActionResult> Find(Guid id, Address address , string filterAddress)
        {
            var dd = _context.Addresses.Where(x => (x.Country + x.Region + x.Person.FullName).Contains(filterAddress)).ToList();

            IEnumerable<Address> OutAddress = dd;
            if (address == null)
            {
                return NotFound();
            }
            else if (address != null)
            {
                return View("Index", OutAddress);
            }
            //return View(OutPers);
            return View("Index", OutAddress);
        }
    }
}
