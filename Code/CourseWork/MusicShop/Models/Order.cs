using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicShop.Models
{
    public class Order
    {
        [Key]
        [Column("o_id")]
        public int Id { get; set; }

        [Column("o_customer")]
        public Customer? Customer { get; set; }

        [Column("o_employee")]
        public Employee Employee { get; set; } = null!;

        [Column("o_status")] 
        public OrderStatus Status { get; set; } = null!;

        [Column("o_payment_type")]
        public PaymentType PaymentType { get; set; } = null!;

        [Column("o_total_cost")]
        public decimal TotalCost { get; set; }

        [Column("o_order_date")]
        public DateTime OrderDate { get; set; }

        [Column("o_payment_date")]
        public DateTime? PaymentDate { get; set; }

        public ICollection<ShoppingCart> ShoppingCarts { get; set;} = new List<ShoppingCart>(); 
    }
}
