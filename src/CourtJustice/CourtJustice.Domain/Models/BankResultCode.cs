using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtJustice.Domain.Models
{
    [Table("bank_result_code")]
    public class BankResultCode : BaseEntity
    {
        [Key]
        public string BankResultCodeId { get; set; }
        public string BankResultCodeName { get; set; }
        public string EmployerCode { get; set; }

        [ForeignKey(nameof(EmployerCode))]
        public virtual Employer? Employer { set; get; }

    }
}