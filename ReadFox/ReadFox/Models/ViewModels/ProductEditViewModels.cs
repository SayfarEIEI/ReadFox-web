namespace ReadFox.Models.ViewModels
{
    public class ProductEditViewModels
    {
        public string ProductName { get; set; }
        public string Author { get; set; }
        public int CategoryId { get; set; }
        public int TypestoryId { get; set; }
        public int? Price { get; set; }
       
        public int Id { get; set; }
    }
}