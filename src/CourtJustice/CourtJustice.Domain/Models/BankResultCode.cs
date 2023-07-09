using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtJustice.Domain.Models
{
    [Table("bank_result_code")]
    public class BankResultCode : BaseEntity
    {
        [Key]
        [Display(Name = "Result Code[��Ҥ��]")]
        public string BankResultCodeId { get; set; }
        [Display(Name = "��͸Ժ��")]
        public string BankResultCodeName { get; set; }
        public string EmployerCode { get; set; }
        [ForeignKey(nameof(EmployerCode))]
        public virtual Employer? Employer { set; get; }

    }
}