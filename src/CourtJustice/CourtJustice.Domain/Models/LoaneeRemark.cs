using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtJustice.Domain.Models
{
    [Table("loanee_remark")]
    public class LoaneeRemark
    {
        [Key]
        public int LoaneeRemarkId { get; set; }
        [Required]
        public string Remark { get; set; }

        public string CusId { get; set; }
        [ForeignKey(nameof(CusId))]
        public virtual Loanee? Loanee { set; get; }


        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy HH:mm}")]
        public DateTime TransactionDatetime { get; set; } = DateTime.Now;

        public string BankActionCodeId { get; set; }
        [ForeignKey(nameof(BankActionCodeId))]
        public virtual BankActionCode? BankActionCode { set; get; }

        public string BankResultCodeId { get; set; }
        [ForeignKey(nameof(BankResultCodeId))]
        public virtual BankResultCode? BankResultCode { set; get; }

        public string CompanyActionCodeId { get; set; }
        [ForeignKey(nameof(CompanyActionCodeId))]
        public virtual CompanyActionCode? CompanyActionCode { set; get; }
        public string CompanyResultCodeId { get; set; }
        [ForeignKey(nameof(CompanyResultCodeId))]
        public virtual CompanyResultCode? CompanyResultCode { set; get; }


        [Display(Name = "เบอร์ติดต่อ")]
        public string? ContractNo { get; set; }

        [Required]
        [Display(Name = "วันนัด")]
        public DateOnly AppointmentDate { get; set; }

        [Display(Name = "ยอดจ่าย")]
        public decimal? Amount { get; set; }

        [Display(Name = "ผู้นัด")]
        public string? AppointmentContract { get; set; }
    }
}
