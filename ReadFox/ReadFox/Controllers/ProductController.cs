using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.FileProviders;
using ReadFox.Models.db_ReadFoxweb;
using ReadFox.Models.ViewModels;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ReadFox.Controllers
{
    public class ProductController : Controller
    {
        private readonly ReadFoxwebContext  _db;

        public ProductController(ReadFoxwebContext db)
        {
            _db = db;
        }
   
        public IActionResult Adds()
        {
            AddBooks();
            return View();
        }
        public  IActionResult AddBooks()
        {
            ViewData["CategoryLists"] = new SelectList(_db.Categorys, "CategoryId", "CategoryName");
            ViewData["StoryLists"] = new SelectList(_db.Tyrestorys, "TypestoryId", "TypestoryName");
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
            ViewData["StoryLists"] = new SelectList(_db.Tyrestorys, "TypestoryId", "TypestoryName",books.TypestoryId);
            return View(books);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var ps = await _db.Books.FindAsync(id);
            _db.Remove(ps);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Detail(string name)
        {
            var pds = await (from p in _db.Books
                             from c in _db.Categorys
                             from t in _db.Tyrestorys
                             where (p.CategoryId == c.CategoryId) && (p.TypestoryId == t.TypestoryId) && (p.ProductName == name)
                             select new ProductViewModels
                             {
                                 ProductName = p.ProductName,
                                 Author = p.Author,
                                 CategoryName = c.CategoryName,
                                 TypestoryName = t.TypestoryName,
                                 Price = p.Price,
                                 ImageName = p.ImageName,
                                 Id = p.Id,
                             }).FirstOrDefaultAsync();
            if (pds == null) { return NotFound(); }
            return View(pds);
        }
        
        public async  Task<IActionResult> Edit(int id)
        {
            var pds = await (from p in _db.Books
                             from c in _db.Categorys
                             from t in _db.Tyrestorys
                             where (p.CategoryId == c.CategoryId) && (p.TypestoryId == t.TypestoryId) && (p.Id == id)
                             select new ProductEditViewModels
                             {
                                 ProductName = p.ProductName,
                                 Author = p.Author,
                                 CategoryId = c.CategoryId,
                                 TypestoryId = t.TypestoryId,
                                 Price = p.Price,
                                 Id = p.Id,
                             }).FirstOrDefaultAsync();
            if (pds == null) { return NotFound(); }
            ViewData["CategoryLists"] = new SelectList(_db.Categorys, "CategoryId", "CategoryName", pds.CategoryId);
            ViewData["StoryLists"] = new SelectList(_db.Tyrestorys, "TypestoryId", "TypestoryName", pds.TypestoryId);
            return View(pds);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id ,ProductEditViewModels data)
        {
            if (id != data.Id) { return NotFound(); }
           
            if (ModelState.IsValid)
            {
                try
                {
                    var ps = await (from p in _db.Books
                                    where (p.Id == id)
                                    select p).FirstOrDefaultAsync();
                    ps.ProductName = data.ProductName;
                    ps.Author = data.Author;
                    ps.CategoryId = data.CategoryId;
                    ps.TypestoryId = data.TypestoryId;
                    ps.Price = data.Price;

                    _db.Update(ps);
                 await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    bool result = _db.Books.Any(p=>p.Id==id);
                    if (result == false)
                    {
                        return NotFound();
                    }
                }
                return RedirectToAction("Index","Home");
            }
            ViewData["CategoryLists"] = new SelectList(_db.Categorys, "CategoryId", "CategoryName", data.CategoryId);
            ViewData["StoryLists"] = new SelectList(_db.Tyrestorys, "TypestoryId", "TypestoryName", data.TypestoryId);
            return View(data);
        }
    }
}
