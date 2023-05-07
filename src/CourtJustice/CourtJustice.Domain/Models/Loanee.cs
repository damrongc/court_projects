using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtJustice.Domain.Models
{
    [Table("loanee")]
    public class Loanee : BaseEntity
    {
        [Key]
        [Display(Name = "ID-CUST")]
        public string CusId { get; set; } = string.Empty;
        [Required]
        [Display(Name = "ชื่อ-สกุล")]
        public string Name { get; set; } = string.Empty;
        [Required]
        [Display(Name = "เบอร์ติดต่อ")]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required]
        [Display(Name = "ที่อยู่ตาม ทร.")]
        public string Address { get; set; } = string.Empty;
        public int AddressId { get; set; }
        [ForeignKey(nameof(AddressId))]
        public virtual AddressSet? AddressSet { set; get; }

        [Display(Name = "ที่อยู่ที่ติดต่อได้(1)")]
        public string? Address1 { get; set; } = string.Empty;
        public int Address1Id { get; set; } = 0;
        [ForeignKey(nameof(Address1Id))]
        public virtual AddressSet? AddressSet1 { set; get; }

        [Display(Name = "ที่อยู่ที่ติดต่อได้(2)")]
        public string? Address2 { get; set; } = string.Empty;
        public int Address2Id { get; set; } = 0;
        [ForeignKey(nameof(Address2Id))]
        public virtual AddressSet? AddressSet2 { set; get; }

        public int OccupationId { get; set; } = 0;
        [ForeignKey(nameof(OccupationId))]
        public virtual Occupation Occupation { set; get; } = new Occupation();
        [Display(Name = "เลขที่สัญญา")]
        public string LoanNumber { get; set; } = string.Empty;

        [Display(Name = "ประเภทสินเชื่อ")]
        public string LoanTypeCode { get; set; } = string.Empty;

        [Display(Name = "ค่างวดตามสัญญา")]
        public decimal InstallmentsByContract { get; set; } = 0;

        [Display(Name = "ค่างวดที่ตกลง")]
        public decimal InstallmentsByAgree { get; set; } = 0;

        [Display(Name = "วันที่ชำระครั้งสุดท้าย")]
        public DateOnly LastPaidDate { get; set; }

        [Display(Name = "สถานะบัญชี")]
        public int BucketId { get; set; }

        [Display(Name = "วันที่กำหนดชำระงวดแรก")]
        public DateOnly FirstPaidDate { get; set; }

        [Display(Name = "อัตราดอกเบี้ยตามสัญญา")]
        public decimal IntereteRate { get; set; } = 0;

        [Display(Name = "จำนวนเงินดอกเบี้ยตามสัญญา")]
        public decimal IntereteRateAmount { get; set; } = 0;

        [Display(Name = "ต้นเงินค้างชำระ")]
        public decimal OverdueAmount { get; set; } = 0;

        [Display(Name = "วันนัดชำระ")]
        public DateOnly DueDate { get; set; }

        [Display(Name = "วัน FollowUp")]
        public DateOnly FollowUpDate { get; set; }

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

        [Display(Name = "รหัสกลุ่มงาน")]
        public int TaskStatusId { get; set; }

        [Display(Name = "รหัสกลุ่มงานย่อย")]
        public int SubTaskStatusId { get; set; }

    }
}
