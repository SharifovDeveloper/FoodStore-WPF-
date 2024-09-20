namespace FoodStore
{
    public interface IProductService
    {
        List<Product> GetAllProducts();
        List<Product> GetProductsByCategory(string category);
    }
    public class ProductService : IProductService
    {
        private readonly List<Product> _products;

        public ProductService()
        {
            _products = new List<Product>
            {
                new Product { Id = 1, Name = "Chicken Masala", Category = "Signature", Price = 10, Count = 100, ImagePath = "images/chicken_masala.png" },
                new Product { Id = 2, Name = "Chicken Tikka Masala", Category = "Signature", Price = 12, Count = 50, ImagePath = "images/chicken_tikka_masala.png" },
                new Product { Id = 3, Name = "Butter Croissant", Category = "Croissant", Price = 5, Count = 200, ImagePath = "images/butter_croissant.png" },
                new Product { Id = 4, Name = "Waffle", Category = "Waffle", Price = 6, Count = 150, ImagePath = "images/waffle.png" }
            };
        }

        public List<Product> GetAllProducts() => _products;

        public List<Product> GetProductsByCategory(string category)
        {
            return _products.Where(p => p.Category == category).ToList();
        }
    }
}
