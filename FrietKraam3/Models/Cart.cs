namespace FrietKraam3.Models
{
    public class Cart
    {
        public int CartId { get; set; }
        public int Quantity { get; set; }
        public DateTime OrderMade { get; set; } = DateTime.Now;
        public List<Product>? Products { get; set; }
        
        //nav prop
        public Product? Product { get; set; }
    }
}
