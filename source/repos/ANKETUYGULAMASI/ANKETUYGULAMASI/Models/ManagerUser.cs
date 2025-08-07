using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ANKETUYGULAMASI.Models
{
    [Table("manager_users")]
    public class ManagerUser
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [Required]
        public string PasswordPlain { get; set; }
    }
}
