using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtJustice.Domain.Models
{
    [Table("loanee_remark")]
    public class LoaneeRemark 
    {
        [Key]
        public int LoaneeRemarkId { get; set; }
        [Required]
        public string Remark { get; set; }

        public string CusId { get; set; }
        [ForeignKey(nameof(CusId))]
        public virtual Loanee? Loanee { set; get; }

    }
}
