using System.ComponentModel.DataAnnotations.Schema;

namespace MusicShop.Models
{
    public class ProductType
    {
        [Column("pt_id")]
        public int Id { get; set; }

        [Column("pt_name")]
        public string Name { get; set; } = null!;

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
