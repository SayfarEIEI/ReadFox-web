using ReadFox.Models.db_ReadFoxweb;
using Microsoft.AspNetCore.Http;

namespace ReadFox.Models.ViewModels
{
    public class AddImgeViewModels
    {
        public string ProductName { get; set; }
        public string Author { get; set; }
        public int? TypestoryId { get; set; }
        public IFormFile formFile { get; set; }
        public int? Price { get; set; }
        public int? CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual Tyrestory Typestory { get; set; }

    }
}
