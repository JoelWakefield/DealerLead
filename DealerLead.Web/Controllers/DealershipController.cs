using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DealerLead.Web.Controllers
{
    [Authorize]
    public class DealershipController : Controller
    {
        private readonly DealerLeadDBContext _context;
        private readonly TokenAuth _tokenAuth;

        public DealershipController(DealerLeadDBContext context, TokenAuth tokenAuth)
        {
            _context = context;
            _tokenAuth = tokenAuth;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Dealership.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var dealership = await _context.Dealership.FirstOrDefaultAsync(d => d.Id == id);

            if (dealership == null)
                return NotFound();

            return View(dealership);
        }

        public IActionResult Create()
        {
            ViewData["Options"] = new SelectList(_context.SupportedState, "Abbreviation", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Street1,Street2,City,State,Zipcode")] Dealership dealership)
        {
            if (ModelState.IsValid)
            {
                dealership.CreateDate = DateTime.Now;
                var user = _tokenAuth.GetUserId(User);

                if (user == null)
                    return NotFound();

                _context.Add(dealership);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["State"] = new SelectList(_context.SupportedState, "Id", "Name", dealership.State);
            return View(dealership);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var dealership = await _context.Dealership.FindAsync(id);
            if (dealership == null)
                return NotFound();

            ViewData["Options"] = new SelectList(_context.SupportedState, "Abbreviation", "Name", dealership.State);
            return View(dealership);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Street1,Street2,City,State,Zipcode,CreatingUserId")] Dealership dealership)
        {
            if (id != dealership.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dealership);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SupportedDealershipExists(dealership.Id))
                        return NotFound();
                    else
                        throw;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["Options"] = new SelectList(_context.SupportedState, "Abbreviation", "Name", dealership.State);
            return View();
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var dealership = await _context.Dealership.FirstOrDefaultAsync(d => d.Id == id);
            if (dealership == null)
                return NotFound();

            return View(dealership);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dealership = await _context.Dealership.FindAsync(id);
            _context.Dealership.Remove(dealership);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SupportedDealershipExists(int id)
        {
            return _context.Dealership.Any(d => d.Id == id);
        }
    }
}
