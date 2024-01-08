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
    [Authorize(Roles = "Admin")]
    //[Authorize]
    public class CashiersController : Controller
    {
        private readonly ShaTaskContext _context;

        public CashiersController(ShaTaskContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Index()
        {
            var shaTaskContext = _context.Cashiers.Include(c => c.Branch);
            return View(await shaTaskContext.ToListAsync());
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cashiers == null)
            {
                return NotFound();
            }

            var cashier = await _context.Cashiers
                .Include(c => c.Branch)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cashier == null)
            {
                return NotFound();
            }

            return View(cashier);
        }


        public IActionResult Create()
        {
            ViewData["BranchId"] = new SelectList(_context.Branches, "Id", "BranchName");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( AddCashierViewModel cashierVM)
        {
            var cashier = new Cashier
            {
                CashierName = cashierVM.CashierName,
                BranchId = cashierVM.BranchId
            };
            if (ModelState.IsValid)
            {
                _context.Add(cashier);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cashierVM);
            
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cashiers == null)
            {
                return NotFound();
            }

            var cashier = await _context.Cashiers.FindAsync(id);
            if (cashier == null)
            {
                return NotFound();
            }
            ViewData["BranchId"] = new SelectList(_context.Branches, "Id", "BranchName", cashier.BranchId);
            ViewData["CashierId"] = cashier.Id;
            return View(cashier);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CashierName,BranchId")] UpdateCasherViewModel cashierVM)
        {
            if (id != cashierVM.Id)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                try
                {
                    var existingCashier = await _context.Cashiers.FindAsync(id);
                    existingCashier.Id = cashierVM.Id;
                    existingCashier.CashierName = cashierVM.CashierName;
                    existingCashier.BranchId = cashierVM.BranchId;

                    _context.Update(existingCashier);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CashierExists(cashierVM.Id))
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
            ViewData["BranchId"] = new SelectList(_context.Branches, "Id", "BranchName", cashierVM.BranchId);
            return View(cashierVM);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cashiers == null)
            {
                return NotFound();
            }

            var cashier = await _context.Cashiers
                .Include(c => c.Branch)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cashier == null)
            {
                return NotFound();
            }

            return View(cashier);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cashiers == null)
            {
                return Problem("Entity set 'ShaTaskContext.Cashiers'  is null.");
            }
            var cashier = await _context.Cashiers.FindAsync(id);
            if (cashier != null)
            {
                _context.Cashiers.Remove(cashier);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CashierExists(int id)
        {
          return (_context.Cashiers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
