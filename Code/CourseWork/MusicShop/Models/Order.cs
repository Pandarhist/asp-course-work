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
        public int? CustomerId { get; set; }

        [Column("o_employee")]
        public int? EmployeeId { get; set; }

        [Column("o_status")]
        public int StatusId { get; set; } = 1;

        [Column("o_payment_type")]
        public int PaymentTypeId { get; set; }

        [Column("o_total_cost")]
        public int TotalCost { get; set; }

        [Column("o_order_date")]
        public DateTime OrderDate { get; set; }

        [Column("o_payment_date")]
        public DateTime? PaymentDate { get; set; }

        [ForeignKey("CustomerId")]
        public Customer? Customer { get; set; }

        [ForeignKey("EmployeeId")]
        public Employee? Employee { get; set; }

        [ForeignKey("StatusId")]
        public OrderStatus? Status { get; set; }

        [ForeignKey("PaymentTypeId")]
        public PaymentType? PaymentType { get; set; }
    }
}
