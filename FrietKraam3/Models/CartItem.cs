namespace FrietKraam3.Models
{
    public class CartItem
    {
        public int CartItemId { get; set; }
        public int Quantity { get; set; }
        public List<Product> Products { get; set; }

        //nav
        public Product Product { get; set; }
        public Cart Cart { get; set; }
    }
}
