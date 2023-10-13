using System.ComponentModel.DataAnnotations;

namespace FrietKraam3.Models
{
    public class Owner
    {
        
        public int OwnerId { get; set; }
       
        public string OwnerName { get; set; } = null!;
    }
}
