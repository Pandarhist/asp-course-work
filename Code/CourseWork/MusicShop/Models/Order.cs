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
        public int CustomerId { get; set; }

        [Column("o_employee")]
        public int EmployeeId { get; set; }

        [Column("o_status")] 
        public int StatusIs { get; set; }

        [Column("o_payment_type")]
        public int PaymentTypeId { get; set; }

        [Column("o_total_cost")]
        public decimal TotalCost { get; set; }

        [Column("o_order_date")]
        public DateTime OrderDate { get; set; }

        [Column("o_payment_date")]
        public DateTime? PaymentDate { get; set; }

        [ForeignKey("o_customer")]
        public Customer? Customer { get; set; }

        [ForeignKey("o_employee")]
        public Employee? Employee { get; set; } = null!;

        [ForeignKey("o_status")]
        public OrderStatus Status { get; set; } = null!;

        [ForeignKey("o_payment_type")]
        public PaymentType PaymentType { get; set; } = null!;

    }
}
