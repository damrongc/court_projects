using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CourtJustice.Domain.Models;

namespace CourtJustice.Domain
{
    [Table("loanee_remark")]
    public class LoaneeRemark : BaseEntity
    {
        [Key]
        public int LoaneeRemarkId { get; set; }
        [Required]
        public string Remark { get; set; }
        [Required]
        public string CusId { get; set; }
        [ForeignKey(nameof(CusId))]
        public virtual Loanee? Loanee { set; get; }

    }
}
