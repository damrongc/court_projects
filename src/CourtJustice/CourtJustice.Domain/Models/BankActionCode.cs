using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtJustice.Domain.Models
{
    [Table("bank_action_code")]
    public class BankActionCode : BaseEntity
    {
        [Key]
        public int BankActionId { get; set; }
        [Display(Name = "Action Code[��Ҥ��]")]
        public string BankActionCodeId { get; set; }
        [Display(Name = "��͸Ժ��")]
        public string BankActionCodeName { get; set; }
        [Display(Name = "�����Ҩ�ҧ/��Ҥ��")]
        public string EmployerCode { get; set; }

        //[ForeignKey(nameof(EmployerCode))]
        //public virtual Employer? Employer { set; get; }

    }
}