using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ecommerce_Laptops;
using Ecommerce_Laptops.Models;
using Microsoft.AspNetCore.Authorization;

namespace Ecommerce_Laptops.Controllers
{
    [Authorize(Roles = "Admin")]
    public class LaptopController : Controller
    {
        private readonly MyAppDbContext _context;

        public LaptopController(MyAppDbContext context)
        {
            _context = context;
        }

        // GET: Laptop
        public async Task<IActionResult> Index()
        {
            return View(await _context.Laptops.ToListAsync());
        }

        // GET: Laptop/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var laptopModel = await _context.Laptops
                .FirstOrDefaultAsync(m => m.ID == id);
            if (laptopModel == null)
            {
                return NotFound();
            }

            return View(laptopModel);
        }

        [Authorize]
        // GET: Laptop/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Laptop/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ImageUrl,Name,Description,Price,RamAndStorage,BatteryCapacity")] LaptopModel laptopModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(laptopModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(laptopModel);
        }

        // GET: Laptop/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var laptopModel = await _context.Laptops.FindAsync(id);
            if (laptopModel == null)
            {
                return NotFound();
            }
            return View(laptopModel);
        }

        // POST: Laptop/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ImageUrl,Name,Description,Price,RamAndStorage,BatteryCapacity")] LaptopModel laptopModel)
        {
            if (id != laptopModel.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(laptopModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LaptopModelExists(laptopModel.ID))
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
            return View(laptopModel);
        }

        // GET: Laptop/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var laptopModel = await _context.Laptops
                .FirstOrDefaultAsync(m => m.ID == id);
            if (laptopModel == null)
            {
                return NotFound();
            }

            return View(laptopModel);
        }

        // POST: Laptop/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var laptopModel = await _context.Laptops.FindAsync(id);
            _context.Laptops.Remove(laptopModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LaptopModelExists(int id)
        {
            return _context.Laptops.Any(e => e.ID == id);
        }
    }
}
