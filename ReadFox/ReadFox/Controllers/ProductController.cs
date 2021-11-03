using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ReadFox.Models.db_ReadFox;
using System.Threading.Tasks;

namespace ReadFox.Controllers
{
    public class ProductController : Controller
    {
        private readonly ReadFoxContext _db;

        public ProductController(ReadFoxContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
        public  IActionResult AddBooks()
        {
            ViewData["CategoryLists"] = new SelectList(_db.Categorys, "CategoryId", "CategoryName");
            ViewData["StoryLists"] = new SelectList(_db.Typestorys, "TypestoryId", "TypestoryName");
            return View("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBooks(Book books)
        {
            if (ModelState.IsValid)
            {
            Book adBook = new Book();
            adBook.ProductName = books.ProductName;
            adBook.CategoryId = books.CategoryId;
            adBook.Price = books.Price;
            adBook.TypestoryId = books.TypestoryId;
            adBook.Author = books.Author;
            adBook.ImageName = books.ImageName;
            _db.Add(adBook);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
            }
            ViewData["CategoryLists"] = new SelectList(_db.Categorys, "CategoryID", "CategoryName", books.CategoryId);
            ViewData["StoryLists"] = new SelectList(_db.Typestorys, "TypestoryId", "TypestoryName",books.TypestoryId);
            return View(books);
        }
        
    }
}
