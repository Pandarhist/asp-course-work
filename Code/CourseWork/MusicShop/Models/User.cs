using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicShop.Models
{
    public class User
    {
        [Key]
        [Column("u_id")]
        public int Id { get; set; }

        [Column("u_login")]
        public string Login { get; set; } = null!;

        [Column("u_password")]
        public string Password { get; set; } = null!;

        public ICollection<Employee> Staff { get; set; } = new List<Employee>();
    }
}
