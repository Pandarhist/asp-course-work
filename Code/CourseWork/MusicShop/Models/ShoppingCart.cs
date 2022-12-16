using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicShop.Models
{
    public class ShoppingCart
    {
        [Key]
        [Column("sc_order")]
        public Order Order { get; set; } = null!;

        [Column("sc_product")]
        public Product Product { get; set; } = null!;

        [Column("sc_count")]
        public int Count { get; set; } = 1;
    }
}
