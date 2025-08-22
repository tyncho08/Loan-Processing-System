using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LPSystemWebAPICore.Models
{
    [Table("UserTable")]
    public class UserTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string UserGender { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string UserEmail { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string UserPass { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string UserRole { get; set; } = string.Empty;
    }
}