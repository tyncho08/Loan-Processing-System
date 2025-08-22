using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LPSystemWebAPICore.Models
{
    [Table("LoanTable")]
    public class LoanTable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LoanId { get; set; }

        [Required]
        [StringLength(50)]
        public string LoanType { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(19,4)")]
        public decimal LoanAmt { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,1)")]
        public decimal LoanROI { get; set; }

        [Required]
        public int LoanPeriod { get; set; }
    }
}