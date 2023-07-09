using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtJustice.Domain.Models
{
    [Table("company_action_code")]
    public class CompanyActionCode : BaseEntity
    {
        [Key]
        [Display(Name = "Action Code[����ѷ]")]
        public string CompanyActionCodeId { get; set; }
        [Display(Name = "��͸Ժ��")]
        public string CompanyActionCodeName { get; set; }
        [Display(Name = "����ѷ")]
        public int CompanyId { get; set; }

        [ForeignKey(nameof(CompanyId))]
        public virtual Company? Company { set; get; }

    }
}