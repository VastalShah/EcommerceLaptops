using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ecommerce_Laptops;
using Ecommerce_Laptops.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Ecommerce_Laptops.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private readonly MyAppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderController(MyAppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Order
        public async Task<IActionResult> Index()
        {
            var userid = _userManager.GetUserId(HttpContext.User);
            List<OrderModel> orders = await _context.Orders.ToListAsync();
            orders = orders.Where(u => u.user_id == userid).ToList();
            return View(orders);
        }

        // GET: Order/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderModel = await _context.Orders
                .FirstOrDefaultAsync(m => m.Order_ID == id);
            if (orderModel == null)
            {
                return NotFound();
            }

            return View(orderModel);
        }

        // GET: Order/Create
        public async Task<IActionResult> Create(int id)
        {
            var userid = _userManager.GetUserId(HttpContext.User);
            var laptop = _context.Laptops.FirstOrDefault(m => m.ID == id);
            var user = await _userManager.GetUserAsync(User);
            OrderDetailsModel orderDetails = new OrderDetailsModel
            {
                Product_id = laptop.ID,
                Product_name = laptop.Name,
                Product_ImageUrl = laptop.ImageUrl,
                Product_Price = laptop.Price,
                User_id = userid,
                UserName = user.UserName,
                UserEmail = user.Email,
                UserPhoneNumber = user.PhoneNumber,
                UserAddress = user.Address
            };
            ViewData["details"] = orderDetails;
            return View();
        }

        // POST: Order/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderModel orderModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(orderModel);
        }

        // GET: Order/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderModel = await _context.Orders.FindAsync(id);
            if (orderModel == null)
            {
                return NotFound();
            }
            return View(orderModel);
        }

        // POST: Order/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,laptop_id,user_id")] OrderModel orderModel)
        {
            if (id != orderModel.Order_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderModelExists(orderModel.Order_ID))
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
            return View(orderModel);
        }

        // GET: Order/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderModel = await _context.Orders
                .FirstOrDefaultAsync(m => m.Order_ID == id);
            if (orderModel == null)
            {
                return NotFound();
            }

            return View(orderModel);
        }

        // POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var orderModel = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(orderModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderModelExists(string id)
        {
            return _context.Orders.Any(e => e.Order_ID == id);
        }
    }
}
