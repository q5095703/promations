using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Promations.Data;
using Promations.Models;

namespace Promations.Controllers
{
    public class PromationsController : Controller
    {
        private readonly DBContext _context;

        public PromationsController(DBContext context)
        {
            _context = context;
        }

        // GET: Promations
        public JsonResult Promations()
        {
            var dBContext = _context.Promations.Include(p => p.product).Where(p => p.product.IsDeleted == false).Where(p=> p.IsDeleted == false);

      
            return Json(dBContext.ToList());
        }

      

        // POST: Promations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create(Promation promation)
        {
          
            if (ModelState.IsValid)
            {
                var item = new Promation();
                item.ProductID = promation.ProductID;
                item.Start = promation.Start;
                item.End = promation.End;
                item.IsDeleted = false;
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction("/Home/Index");
            }
            ViewData["ProductID"] = new SelectList(_context.Products, "ID", "ID", promation.ProductID);
            return RedirectToAction("/Home/Index");
        }

        

        // POST: Promations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Edit(Promation promation)
        {
          
            if (ModelState.IsValid)
            {
                try
                {
                    var item = new Promation();
                    item.ProductID = promation.ProductID;
                    item.Start = promation.Start;
                    item.End = promation.End;
                    item.ID = promation.ID;
                    item.IsDeleted = false;
                    _context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PromationExists(promation.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("/Home/Index");
            }
            ViewData["ProductID"] = new SelectList(_context.Products, "ID", "ID", promation.ProductID);
            return View(promation);
        }
        

        // POST: Promations/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var promation = await _context.Promations.SingleOrDefaultAsync(m => m.ID == id);
            // _context.Promations.Remove(promation);
            promation.IsDeleted = true;

            await _context.SaveChangesAsync();
            return RedirectToAction("/Home/Index");
        }

        private bool PromationExists(int id)
        {
            return _context.Promations.Any(e => e.ID == id);
        }
    }
}
