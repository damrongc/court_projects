using CourtJustice.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace CourtJustice.Domain
{
    public class LoaneeRemark :BaseEntity
    {
        [Key]
        public int LoaneeRemarkId { get; set; }
        [Required]
        public string Remark { get; set; }
        [Required]
        public string CusId { get; set; }


    }
}
