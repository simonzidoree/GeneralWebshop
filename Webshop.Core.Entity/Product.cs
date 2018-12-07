namespace Webshop.Core.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
        public int AmountInStock { get; set; }
        public bool Featured { get; set; }
        public Order Order { get; set; }
    }
}