using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtJustice.Domain.Models
{
    [Table("company_result_code")]
    public class CompanyResultCode : BaseEntity
    {
        [Key]
        public string CompanyResultCodeId { get; set; }
        public string CompanyResultCodeName { get; set; }

        public bool NotCallFlag { get; set; }
        public bool ShowHideFlag { get; set; }
        public int CompanyId { get; set; }

        [ForeignKey(nameof(CompanyId))]
        public virtual Company? Company { set; get; }

    }
}