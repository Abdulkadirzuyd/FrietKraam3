using System.ComponentModel.DataAnnotations;

namespace FrietKraam3.Models
{
    public class Customer
    {
        public int CustomerId { get; set; } 
       
        public string CustomerName { get; set; } = null!;
        
        public ICollection<Order> Orders { get; set; } = null!;
    }
}
