using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShoroukAcademy.ShaTask.Models.Models;
using ShoroukAcademy.ShaTask.Presentation.ViewModels;

namespace ShoroukAcademy.ShaTask.Presentation.Controllers
{
    [Authorize]
    public class InvoiceDetailsController : Controller
    {
        private readonly ShaTaskContext _context;

        public InvoiceDetailsController(ShaTaskContext context)
        {
            _context = context;
        }

        // GET: InvoiceDetails
        public async Task<IActionResult> Index()
        {
            var shaTaskContext = _context.InvoiceDetails.Include(i => i.InvoiceHeader);
            return View(await shaTaskContext.ToListAsync());
        }

        // GET: InvoiceDetails/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.InvoiceDetails == null)
            {
                return NotFound();
            }

            var invoiceDetail = await _context.InvoiceDetails
                .Include(i => i.InvoiceHeader)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invoiceDetail == null)
            {
                return NotFound();
            }

            return View(invoiceDetail);
        }

        
        public IActionResult Create()
        {
            ViewData["InvoiceHeaderId"] = new SelectList(_context.InvoiceHeaders, "Id", "CustomerName");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,InvoiceHeaderId,ItemName,ItemCount,ItemPrice")] AddInvoiceViewModel invoiceDetail)
        {
            if (ModelState.IsValid)
            {
                var invoice = new InvoiceDetail
                {
                    InvoiceHeaderId = invoiceDetail.InvoiceHeaderId,
                    ItemName = invoiceDetail.ItemName,
                    ItemCount = invoiceDetail.ItemCount,
                    ItemPrice = invoiceDetail.ItemPrice
                };
                _context.Add(invoice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            };
            return View(invoiceDetail);
        }

        // GET: InvoiceDetails/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.InvoiceDetails == null)
            {
                return NotFound();
            }

            var invoiceDetail = await _context.InvoiceDetails.FindAsync(id);
            if (invoiceDetail == null)
            {
                return NotFound();
            }
            ViewData["InvoiceHeaderId"] = new SelectList(_context.InvoiceHeaders, "Id", "CustomerName", invoiceDetail.InvoiceHeaderId);
            return View(invoiceDetail);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,InvoiceHeaderId,ItemName,ItemCount,ItemPrice")] UpdateInvoiceViewModel invoiceDetail)
        {
            if (id != invoiceDetail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var existingInvoice = await _context.InvoiceDetails.FindAsync(id);
                        existingInvoice.ItemName = invoiceDetail.ItemName;
                    existingInvoice.ItemPrice = invoiceDetail.ItemPrice;
                    existingInvoice.ItemCount = invoiceDetail.ItemCount;
                    existingInvoice.InvoiceHeaderId = invoiceDetail.InvoiceHeaderId;
                    _context.Update(existingInvoice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InvoiceDetailExists(invoiceDetail.Id))
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
            ViewData["InvoiceHeaderId"] = new SelectList(_context.InvoiceHeaders, "Id", "CustomerName", invoiceDetail.InvoiceHeaderId);
            return View(invoiceDetail);
        }

        // GET: InvoiceDetails/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.InvoiceDetails == null)
            {
                return NotFound();
            }

            var invoiceDetail = await _context.InvoiceDetails
                .Include(i => i.InvoiceHeader)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (invoiceDetail == null)
            {
                return NotFound();
            }

            return View(invoiceDetail);
        }

        // POST: InvoiceDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.InvoiceDetails == null)
            {
                return Problem("Entity set 'ShaTaskContext.InvoiceDetails'  is null.");
            }
            var invoiceDetail = await _context.InvoiceDetails.FindAsync(id);
            if (invoiceDetail != null)
            {
                _context.InvoiceDetails.Remove(invoiceDetail);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InvoiceDetailExists(long id)
        {
          return (_context.InvoiceDetails?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
