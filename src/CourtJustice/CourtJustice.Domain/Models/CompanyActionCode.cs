using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtJustice.Domain.Models
{
    [Table("company_action_code")]
    public class CompanyActionCode
    {
        [Key]
        public string CompanyActionCodeId { get; set; }
        public string CompanyActionName { get; set; }
        public int CompanyId { get; set; }

        [ForeignKey(nameof(CompanyId))]
        public virtual Company? Company { set; get; }

    }
}