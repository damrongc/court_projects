using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtJustice.Domain.Models
{
    [Table("loanee_company_information")]
    public class LoaneeCompanyInformation
    {
        [Key]
        public int LoaneeCompanyId { get; set; }

        [Display(Name = "วัตถุประสงค์")]
        public string Objective { get; set; }

        [Display(Name = "กรรมการผู้จัดการ 1")]
        public string? Director1 { get; set; }
        [Display(Name = "กรรมการผู้จัดการ 2")]
        public string? Director2 { get; set; }
        [Display(Name = "กรรมการผู้จัดการ 3")]
        public string? Director3 { get; set; }
        [Display(Name = "กรรมการผู้จัดการ 4")]
        public string? Director4 { get; set; }
        [Display(Name = "กรรมการผู้จัดการ 5")]
        public string? Director5 { get; set; }
        [Display(Name = "กรรมการผู้จัดการ 6")]
        public string? Director6 { get; set; }
        [Display(Name = "กรรมการผู้จัดการ 7")]
        public string? Director7 { get; set; }
        [Display(Name = "กรรมการผู้จัดการ 8")]
        public string? Director8 { get; set; }

        public string CusId { get; set; }
        [ForeignKey(nameof(CusId))]
        public virtual Loanee? Loanee { set; get; }
    }
}