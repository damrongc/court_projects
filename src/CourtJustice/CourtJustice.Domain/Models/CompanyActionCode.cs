using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtJustice.Domain.Models
{
    [Table("company_action_code")]
    public class CompanyActionCode : BaseEntity
    {
        [Key]
        public int CompanyActionId { get; set; }
        [Display(Name = "Action Code[∫√‘…—∑]")]
        public string CompanyActionCodeId { get; set; }
        [Display(Name = "§”Õ∏‘∫“¬")]
        public string CompanyActionCodeName { get; set; }
        [Display(Name = "∫√‘…—∑")]
        public int CompanyId { get; set; }
        
        //[ForeignKey(nameof(CompanyId))]
        //public virtual Company? Company { set; get; }

    }
}