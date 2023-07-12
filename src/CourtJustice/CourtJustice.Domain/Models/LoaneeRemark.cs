using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtJustice.Domain.Models
{
    [Table("loanee_remark")]
    public class LoaneeRemark
    {
        [Key]
        public int LoaneeRemarkId { get; set; }
        public string CusId { get; set; }
        [ForeignKey(nameof(CusId))]
        public virtual Loanee? Loanee { set; get; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy HH:mm}")]
        public DateTime TransactionDatetime { get; set; } = DateTime.Now;

        [Display(Name = "Action Code[ธนาคาร]")]
        public string BankActionCodeId { get; set; }
        //[ForeignKey(nameof(BankActionCodeId))]
        //public virtual BankActionCode? BankActionCode { set; get; }

        [Display(Name = "Result Code[ธนาคาร]")]
        public string BankResultCodeId { get; set; }
        //[ForeignKey(nameof(BankResultCodeId))]
        //public virtual BankResultCode? BankResultCode { set; get; }

        [Display(Name = "Action Code[บริษัท]")]
        public string CompanyActionCodeId { get; set; }
        //[ForeignKey(nameof(CompanyActionCodeId))]
        //public virtual CompanyActionCode? CompanyActionCode { set; get; }

        [Display(Name = "Result Code[บริษัท]")]
        public string CompanyResultCodeId { get; set; }
        //[ForeignKey(nameof(CompanyResultCodeId))]
        //public virtual CompanyResultCode? CompanyResultCode { set; get; }

        [Display(Name = "เบอร์ติดต่อ")]
        public string? FollowContractNo { get; set; }

        [Display(Name = "วันที่นัด")]
        public DateOnly? AppointmentDate { get; set; }

        [Display(Name = "ยอดจ่าย")]
        public decimal? Amount { get; set; }

        [Display(Name = "ผู้นัด")]
        public string? AppointmentContract { get; set; }
        [Required]
        [Display(Name = "หมายเหตุ")]
        public string Remark { get; set; }
        public bool IsActive { get; set; }
        public DateTime? UpdateDatetime { get; set; }
        
        [Required]
        public string EmployerCode { get; set; }
        //public string PersonCodeId { get; set; }
        public string BankPersonCodeId { get; set; }

        public int BankActionId { get; set; }
        public int BankResultId { get; set; }
        public int BankPersonId { get; set; }
    }
}
