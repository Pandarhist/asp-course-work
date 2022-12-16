using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicShop.Models
{
    public class OrderStatus
    {
        [Key]
        [Column("os_id")]
        public int Id { get; set; }

        [Column("os_name")]
        public string Name { get; set; } = null!;

        public ICollection<Order> Orders = new List<Order>();
    }
}
