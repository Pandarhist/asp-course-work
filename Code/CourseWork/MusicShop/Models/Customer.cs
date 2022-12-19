using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicShop.Models
{
    public class Customer
    {
        [Key]
        [Column("c_id")]
        public int Id { get; set; }

        [Column("c_surname")]
        public string Surname { get; set; } = null!;

        [Column("c_name")]
        public string Name { get; set; } = null!;

        [Column("c_patronymic")]
        public string Patronymic { get; set; } = null!;

        [Column("c_phone")]
        public string PhoneNumber { get; set; } = null!;
    }
}
