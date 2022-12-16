using System.ComponentModel.DataAnnotations.Schema;

namespace MusicShop.Models
{
    public class PaymentType
    {
        [Column("pt_id")]
        public int Id { get; set; }

        [Column("pt_name")]
        public string Name { get; set; } = null!;

        public ICollection<Order> Orders = new List<Order>();
    }
}
