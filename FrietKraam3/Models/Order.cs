using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;

namespace FrietKraam3.Models
{
    public class Order
    {
        
        public int OrderId { get; set; }
        
        public double TotalPrice { get; set; }
        public virtual Customer? Customer { get; set; }
        public int CustomerId { get; set; } 
        public List<CartItem> CartItems { get; set; }

        
    }
}
