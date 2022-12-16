using System.ComponentModel.DataAnnotations.Schema;

namespace MusicShop.Models
{
    public class Employee
    {
        [Column("s_personnel_number")]
        public int Id { get; set; }

        [Column("s_user")]
        public int? UserId { get; set; }

        [Column("s_surname")]
        public string Surname { get; set; } = null!;

        [Column("s_name")]
        public string Name { get; set; } = null!;

        [Column("s_patronymic")]
        public string Patronymic { get; set; } = null!;

        [Column("s_post")]
        public string Post { get; set; } = null!;

        [Column("s_is_fired")]
        public bool IsFired { get; set; } = false;

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
