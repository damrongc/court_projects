using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtJustice.Domain.Models
{
    [Table("bank_result_code")]
    public class BankResultCode
    {
        [Key]
        public int BankResultId { get; set; }
        [Display(Name = "Result Code[ธนาคาร]")]
        public string BankResultCodeId { get; set; }
        [Display(Name = "คำอธิบาย")]
        public string BankResultCodeName { get; set; }
        //public string EmployerCode { get; set; }
        //[ForeignKey(nameof(EmployerCode))]
        //public virtual Employer? Employer { set; get; }
        public int BankPersonId { get; set; }
        public bool IsActive { get; set; } = true;

    }
}