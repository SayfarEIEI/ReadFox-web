using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.FileProviders;
using ReadFox.Models.db_ReadFox;
using ReadFox.Models.ViewModels;
using System;
using System.IO;
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
        public IActionResult Adds()
        {
            AddBooks();
            return View();
        }
        public  IActionResult AddBooks()
        {
            ViewData["CategoryLists"] = new SelectList(_db.Categorys, "CategoryId", "CategoryName");
            ViewData["StoryLists"] = new SelectList(_db.Typestorys, "TypestoryId", "TypestoryName");
            return View("Adds");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBooks(AddImgeViewModels books )
        {
            if (ModelState.IsValid)
            {
            Book adBook = new Book();
            adBook.ProductName = books.ProductName;
            adBook.CategoryId = books.CategoryId;
            adBook.Price = books.Price;
            adBook.TypestoryId = books.TypestoryId;
            adBook.Author = books.Author;
                string fileName = Path.GetFileName(books.formFile.FileName);
                string fileExt = Path.GetExtension(fileName);
                string tmpName = Guid.NewGuid().ToString();
                string newFileName = string.Concat(tmpName, fileExt);
                string filePath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "image")).Root + $@"{newFileName}";

                using (FileStream fs = System.IO.File.Create(filePath))
                {
                    books.formFile.CopyTo(fs);
                    fs.Flush();
                }
                adBook.ImageName = newFileName;
                _db.Add(adBook);
            await _db.SaveChangesAsync();
            return RedirectToAction("Adds");
            }
            ViewData["CategoryLists"] = new SelectList(_db.Categorys, "CategoryID", "CategoryName", books.CategoryId);
            ViewData["StoryLists"] = new SelectList(_db.Typestorys, "TypestoryId", "TypestoryName",books.TypestoryId);
            return View(books);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string name)
        {
            var ps = _db.Books.FindAsync(name);
            _db.Remove(ps);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        
    }
}
