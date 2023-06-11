using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtJustice.Domain.Models
{
    [Table("bank_action_code")]
    public class BankActionCode
    {
        [Key]
        public string BankActionCodeId { get; set; }
        public string BankActionCodeName { get; set; }
        public string EmployerCode { get; set; }

        [ForeignKey(nameof(EmployerCode))]
        public virtual Employer? Employer { set; get; }

    }
}