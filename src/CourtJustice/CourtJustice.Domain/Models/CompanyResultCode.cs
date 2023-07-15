using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtJustice.Domain.Models
{
    [Table("company_result_code")]
    public class CompanyResultCode : BaseEntity
    {
        [Key]
        public int CompanyResultId { get; set; }
        [Display(Name = "Result Code[∫√‘…—∑]")]
        public string CompanyResultCodeId { get; set; }
        [Display(Name = "§”Õ∏‘∫“¬")]
        public string CompanyResultCodeName { get; set; }
        public bool NotCallFlag { get; set; }
        public bool ShowHideFlag { get; set; }
        [Display(Name = "∫√‘…—∑")]
        public int CompanyId { get; set; }
        public int CompanyActionId { get; set; }
        //[ForeignKey(nameof(CompanyId))]
        //public virtual Company? Company { set; get; }

    }
}