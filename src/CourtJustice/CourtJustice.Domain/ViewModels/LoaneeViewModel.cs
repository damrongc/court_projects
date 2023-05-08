using System.ComponentModel.DataAnnotations;

namespace CourtJustice.Domain.ViewModels
{
    public class LoaneeViewModel
    {
        [Display(Name = "ID-CUST")]
        public string CusId { get; set; } = string.Empty;
        [Display(Name = "ชื่อ-สกุล")]
        public string Name { get; set; } = string.Empty;
        [Display(Name = "เบอร์ติดต่อ")]
        public string PhoneNumber { get; set; } = string.Empty;
        [Display(Name = "ที่อยู่ตาม ทร.")]
        public string Address { get; set; } = string.Empty;
        [Display(Name = "ที่อยู่ที่ติดต่อได้(1)")]
        public string Address1 { get; set; } = string.Empty;
        [Display(Name = "ที่อยู่ที่ติดต่อได้(2)")]
        public string Address2 { get; set; } = string.Empty;
        [Display(Name = "อาชีพ")]
        public string OccupationName { get; set; } = string.Empty;

        [Display(Name = "เลขที่สัญญา")]
        public string LoanNumber { get; set; } = string.Empty;

        [Display(Name = "ประเภทสินเชื่อ")]
        public string LoanTypeName { get; set; } = string.Empty;

        [Display(Name = "ค่างวดตามสัญญา")]
        public decimal InstallmentsByContract { get; set; } = 0;

        [Display(Name = "ค่างวดที่ตกลง")]
        public decimal InstallmentsByAgree { get; set; } = 0;

        [Display(Name = "วันที่ชำระครั้งสุดท้าย")]
        public DateTime LastPaidDate { get; set; }

        [Display(Name = "สถานะบัญชี")]
        public string BucketName { get; set; } = string.Empty;

        [Display(Name = "วันที่กำหนดชำระงวดแรก")]
        public DateTime FirstPaidDate { get; set; }

        [Display(Name = "อัตราดอกเบี้ยตามสัญญา")]
        public decimal IntereteRate { get; set; } = 0;

        [Display(Name = "จำนวนเงินดอกเบี้ยตามสัญญา")]
        public decimal IntereteRateAmount { get; set; } = 0;

        [Display(Name = "ต้นเงินค้างชำระ")]
        public decimal OverdueAmount { get; set; } = 0;

        [Display(Name = "วันนัดชำระ")]
        public DateTime DueDate { get; set; }

        [Display(Name = "วัน FollowUp")]
        public DateTime FollowUpDate { get; set; }

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

        [Display(Name = "ผู้ว่าจ้าง")]
        public string EmployerCode { get; set; } = string.Empty;

        [Display(Name = "OD")]
        public int OverdueDayAmount { get; set; } = 0;
    }
}
