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
    public class TypeDocumentsController : Controller
    {
        private readonly CustomersContext _context;

        public TypeDocumentsController(CustomersContext context)
        {
            _context = context;
        }

        // GET: TypeDocuments
        public async Task<IActionResult> Index()
        {
              return _context.TypeDocuments != null ? 
                          View(await _context.TypeDocuments.ToListAsync()) :
                          Problem("Entity set 'CustomersContext.TypeDocuments'  is null.");
        }

        // GET: TypeDocuments/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.TypeDocuments == null)
            {
                return NotFound();
            }

            var typeDocument = await _context.TypeDocuments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeDocument == null)
            {
                return NotFound();
            }

            return View(typeDocument);
        }

        // GET: TypeDocuments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TypeDocuments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CodeDocument,CountryDocument,CodeCountryDocument,DescriptionRu,DecriptionEn")] TypeDocument typeDocument)
        {
            if (ModelState.IsValid)
            {
                typeDocument.Id = Guid.NewGuid();
                _context.Add(typeDocument);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(typeDocument);
        }

        // GET: TypeDocuments/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.TypeDocuments == null)
            {
                return NotFound();
            }

            var typeDocument = await _context.TypeDocuments.FindAsync(id);
            if (typeDocument == null)
            {
                return NotFound();
            }
            return View(typeDocument);
        }

        // POST: TypeDocuments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,CodeDocument,CountryDocument,CodeCountryDocument,DescriptionRu,DecriptionEn")] TypeDocument typeDocument)
        {
            if (id != typeDocument.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typeDocument);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypeDocumentExists(typeDocument.Id))
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
            return View(typeDocument);
        }

        // GET: TypeDocuments/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.TypeDocuments == null)
            {
                return NotFound();
            }

            var typeDocument = await _context.TypeDocuments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (typeDocument == null)
            {
                return NotFound();
            }

            return View(typeDocument);
        }

        // POST: TypeDocuments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.TypeDocuments == null)
            {
                return Problem("Entity set 'CustomersContext.TypeDocuments'  is null.");
            }
            var typeDocument = await _context.TypeDocuments.FindAsync(id);
            if (typeDocument != null)
            {
                _context.TypeDocuments.Remove(typeDocument);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypeDocumentExists(Guid id)
        {
          return (_context.TypeDocuments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        [HttpPost]
        public async Task<IActionResult> Find(Guid id, TypeDocument typeDocument, string filterDocuments)
        {
            var dd = _context.TypeDocuments.Where(x => (x.CodeDocument + x.CodeCountryDocument).Contains(filterDocuments)).ToList();

            IEnumerable<TypeDocument> OutDoc = dd;
            if (typeDocument == null)
            {
                return NotFound();
            }
            else if (typeDocument != null)
            {
                return View("Index", OutDoc);
            }
            //return View(OutPers);
            return View("Index", OutDoc);
        }
    }
}
