namespace FoodStore
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public string ImagePath { get; set; } // Path to the product image
    }
}
