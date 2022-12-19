using System.ComponentModel.DataAnnotations.Schema;

namespace MusicShop.Models
{
    public class ShoppingCart
    {
        [Column("sc_order")]
        public int OrderId { get; set; }

        [Column("sc_product")]
        public int ProductId { get; set; }

        [Column("sc_count")]
        public int Count { get; set; } = 1;

        [ForeignKey("OrderId")]
        public Order? Order { get; set; }

        [ForeignKey("ProductId")]
        public Product? Product { get; set; }

    }
}
