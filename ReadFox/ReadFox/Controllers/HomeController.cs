using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ReadFox.Models;
using ReadFox.Models.db_ReadFoxweb;
using ReadFox.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ReadFox.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ReadFoxwebContext _db;
        public HomeController(ReadFoxwebContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var detail = from b in _db.Books
                         from c in _db.Categorys
                         from t in _db.Tyrestorys
                         where (b.CategoryId == c.CategoryId) && (b.TypestoryId == t.TypestoryId)
                         select new ProductViewModels
                         {
                             ProductName = b.ProductName,
                             Author = b.Author,
                             CategoryName = c.CategoryName,
                             TypestoryName = t.TypestoryName,
                             Price = b.Price,
                             Id = b.Id,
                         };
            if (detail == null)
            {
                return NotFound();
            }
            return View(await detail.ToListAsync());
        }

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Add()
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
