using System.ComponentModel.DataAnnotations;

namespace FrietKraam3.Models
{
    public class Product
    {
        
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        [DataType(DataType.Currency)]
        public double ProductPrice { get; set; }

    }
}
