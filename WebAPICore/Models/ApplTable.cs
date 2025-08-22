using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LPSystemWebAPICore.Models
{
    [Table("ApplTable")]
    public class ApplTable
    {
        [Key]
        [Column(Order = 0)]
        public int AppId { get; set; }

        [Key]
        [Column(Order = 1)]
        public int UserId { get; set; }

        [Key]
        [Column(Order = 2)]
        public int LoanId { get; set; }

        [Required]
        [StringLength(50)]
        public string UserMob { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string UserAdhaar { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(19,4)")]
        public decimal LoanAmt { get; set; }

        [Required]
        [StringLength(50)]
        public string AppStatus { get; set; } = string.Empty;

        // Navigation properties
        [ForeignKey("UserId")]
        public virtual UserTable? User { get; set; }

        [ForeignKey("LoanId")]
        public virtual LoanTable? Loan { get; set; }
    }
}