using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtJustice.Domain.Models
{
    [Table("loanee")]
    public class Loanee : BaseEntity
    {
        [Key]
        [Display(Name = "เลขที่ผู้เช่าซื้อ")]
        public string CusId { get; set; } = string.Empty;
        [Display(Name = "วันส่งงาน")]
        public DateOnly AssignDate { get; set; }
        [Display(Name = "วันหมดอายุงาน")]
        public DateOnly ExpireDate { get; set; }
        [Display(Name = "วันเดือนปีเกิด")]
        public DateOnly BirthDate { get; set; }
        [Display(Name = "เลขที่บัตรประชาชน")]
        public string NationalityId { get; set; } = string.Empty;
        [Required]
        [Display(Name = "ชื่อ-สกุล")]
        public string Name { get; set; } = string.Empty;
        [Required]
        [Display(Name = "เบอร์ติดต่อ")]
        public string TelephoneHome { get; set; } = string.Empty;

        public int OccupationId { get; set; }
        [ForeignKey(nameof(OccupationId))]
        public virtual Occupation? Occupation { set; get; }
        [Display(Name = "เลขที่สัญญา")]
        public string ContractNo { get; set; } = string.Empty;
        [Display(Name = "วันทำสัญญา")]
        public DateOnly ContractDate { get; set; } = DateOnly.FromDateTime(DateTime.Today);
        [Display(Name = "วันที่ตกเป็นหนี้สูญ")]
        public DateOnly? WODate { get; set; }
        [Display(Name = "จำนวนงวดที่ทำสัญญา")]
        public int Term { get; set; }
        [Display(Name = "เงินต้น")]
        public decimal LoanAmount { get; set; }
        [Display(Name = "ยอดมูลหนี้ ณ.ปัจจุบัน")]
        public decimal WOBalance { get; set; }

        public decimal OverdueAmount { get; set; }
        public decimal TotalPenalty { get; set; }
        public decimal ClosingAmount { get; set; }
        public string? RcvAmtStatus { get; set; }

        [Display(Name = "ยอดชำระก่อนตัดเป็นหนี้สูญ")]
        public decimal RcvAmtBeforeWO { get; set; }
        [Display(Name = "ยอดชำระหลังตัดเป็นหนี้สูญ")]
        public decimal RcvAmtAfterWO { get; set; }
        [Display(Name = "จำนวนเงินที่จ่ายครั้งสุดท้าย")]
        public decimal LastPaidAmount { get; set; }
        [Display(Name = "จำนวนครั้งที่ Assign")]
        public int NoOfAssignment { get; set; }
        
        public string? Description { get; set; } = string.Empty;

        [Display(Name = "ประเภทสินเชื่อ")]
        public string LoanTypeCode { get; set; } = string.Empty;
        [Display(Name = "ค่างวดตามสัญญา")]
        public decimal InstallmentsByContract { get; set; } = 0;
        [Display(Name = "ค่างวดที่ตกลง")]
        public decimal InstallmentsByAgree { get; set; } = 0;
        [Display(Name = "วันที่ชำระครั้งสุดท้าย")]
        public DateOnly? LastPaidDate { get; set; }
        [Display(Name = "สถานะบัญชี")]
        public int BucketId { get; set; }
        [Display(Name = "วันที่กำหนดชำระงวดแรก")]
        public DateOnly? FirstPaidDate { get; set; }
        [Display(Name = "อัตราดอกเบี้ยตามสัญญา")]
        public decimal IntereteRate { get; set; } = 0;
        [Display(Name = "จำนวนเงินดอกเบี้ยตามสัญญา")]
        public decimal IntereteRateAmount { get; set; } = 0;
        //[Display(Name = "ต้นเงินค้างชำระ")]
        //public decimal OverdueAmount { get; set; } = 0;
        [Display(Name = "วันนัดชำระ")]
        public DateOnly? DueDate { get; set; }
        [Display(Name = "วัน FollowUp")]
        public DateOnly? FollowUpDate { get; set; }
        [Display(Name = "จำนวนเงินที่นัดชำระ")]
        public decimal PaidAmount { get; set; } = 0;
        [Display(Name = "จำนวนเงินที่ชำระภายในเดือน")]
        public decimal PaidInMonthAmount { get; set; } = 0;
        [Display(Name = "จำนวนเงินที่ชำระรวม")]
        public decimal TotalAmount { get; set; } = 0;
        [Display(Name = "พนักงานที่รับผิดชอบ")]
        public string EmployeeCode { get; set; } = string.Empty;
        [Display(Name = "ยอดหนี้คงเหลือ")]
        public decimal RemainingAmount { get; set; } = 0;
        [Display(Name = "OD")]
        public int OverdueDayAmount { get; set; } = 0;
        [Display(Name = "ผู้ว่าจ้าง")]
        public string EmployerCode { get; set; } = string.Empty;
        [Display(Name = "กลุ่มงาน")]
        public int LoanTaskStatusId { get; set; }

        public string? HomeAddress1 { get; set; } = string.Empty;
        public string? HomeAddress2 { get; set; } = string.Empty;
        public string? HomeAddress3 { get; set; } = string.Empty;
        public string? HomeAddress4 { get; set; } = string.Empty;
        public string? OfficeAddress1 { get; set; } = string.Empty;
        public string? OfficeAddress2 { get; set; } = string.Empty;
        public string? OfficeAddress3 { get; set; } = string.Empty;
        public string? OfficeAddress4 { get; set; } = string.Empty;
        public string? TelephoneOffice { get; set; } = string.Empty;
        public string? IdenAddress1 { get; set; }
        public string? IdenAddress2 { get; set; }
        public string? IdenAddress3 { get; set; }
        public string? IdenAddress4 { get; set; }
        public string? MobileHome { get; set; }
        public string? MobileOffice { get; set; }
        public string? MobileEmg { get; set; }
        public string? SpecialNote { get; set; }
        public string? CPCase { get; set; }

        public int NoOfCP { get; set; }

        public DateOnly? CPDate { get; set; }

        public decimal OAFee { get; set; }

        public decimal MaxOAFeeAmount { get; set; }
        public decimal MaxOAFeeBalance { get; set; }
        public string? OAFlag { get; set; }
        public string? SendingAddress { get; set; } = string.Empty;


    }
}
