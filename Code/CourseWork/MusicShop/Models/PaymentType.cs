using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicShop.Models
{
    public class PaymentType
    {
        [Key]
        [Column("pt_id")]
        public int Id { get; set; }

        [Column("pt_name")]
        public string Name { get; set; } = null!;
    }
}
