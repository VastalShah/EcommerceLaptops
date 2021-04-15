using Ecommerce_Laptops.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce_Laptops.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MyAppDbContext _context;

        public HomeController(ILogger<HomeController> logger, MyAppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Laptops.ToListAsync());
        }

        public async Task<IActionResult> ViewDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var laptopModel = await _context.Laptops.FirstOrDefaultAsync(m => m.ID == id);

            if (laptopModel == null)
            {
                return NotFound();
            }

            return View(laptopModel);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
