using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicShop.Models
{
    public class Product
    {
        [Key]
        [Column("p_number")]
        public int Id { get; set; }

        [Column("p_type")]
        public int TypeId { get; set; }

        [Column("p_name")]
        public string Name { get; set; } = null!;

        [Column("p_producer")]
        public string Producer { get; set; } = null!;

        [Column("p_price")]
        public decimal Price { get; set; } = 0;

        [Column("p_description")]
        public string? Description { get; set; }

        [Column("p_amount")]
        public int Amount { get; set; } = 0;

        [Column("p_is_deleted")]
        public bool IsDeleted { get; set; } = false;

        [ForeignKey("p_type")]
        public ProductType Type { get; set; } = null!;
    }
}
