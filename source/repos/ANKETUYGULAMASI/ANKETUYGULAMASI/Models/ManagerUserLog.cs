using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ANKETUYGULAMASI.Models
{
    [Table("manager_user_logs")]
    public class ManagerUserLog
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Action { get; set; } // "Login" veya "Logout"
        [Required]
        public DateTime LogTime { get; set; } = DateTime.Now;
    }
}
