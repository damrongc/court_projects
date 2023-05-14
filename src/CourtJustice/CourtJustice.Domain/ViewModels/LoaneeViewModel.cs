using CourtJustice.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtJustice.Domain.ViewModels
{
    public class LoaneeViewModel
    {
        [Display(Name = "เลขที่ผู้เช่าซื้อ")]
        public string CusId { get; set; } = string.Empty;
        [Display(Name = "วันส่งงาน")]
        public DateTime AssignDate { get; set; }

        [Display(Name = "วันหมดอายุงาน")]
        public DateTime ExpireDate { get; set; }
        [Display(Name = "วันเดือนปีเกิด")]
        public DateTime BirthDate { get; set; }
        [Display(Name = "เลขที่บัตรประชาชน")]
        public string NationalityId { get; set; } = string.Empty;
        [Required]
        [Display(Name = "ชื่อ-สกุล")]
        public string Name { get; set; } = string.Empty;
        [Required]
        [Display(Name = "เบอร์ติดต่อ")]
        public string TelephoneHome { get; set; } = string.Empty;
        [Display(Name = "อาชีพ")]
        public int OccupationName { get; set; }
        [Display(Name = "เลขที่สัญญา")]
        public string ContractNo { get; set; } = string.Empty;
        [Display(Name = "วันทำสัญญา")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime ContractDate { get; set; }
        [Display(Name = "วันที่ตกเป็นหนี้สูญ")]
        public DateTime WODate { get; set; }
        [Display(Name = "จำนวนงวดที่ทำสัญญา")]
        public int Term { get; set; }
        [Display(Name = "เงินต้น")]
        [DisplayFormat(DataFormatString = "{0:###,###.##}")]
        public decimal LoanAmount { get; set; }
        [Display(Name = "ยอดมูลหนี้ ณ.ปัจจุบัน")]
        public decimal WOBalance { get; set; }

        public decimal OverdueAmount { get; set; }
        public decimal TotalPenalty { get; set; }
        public decimal ClosingAmount { get; set; }
        public string RcvAmtStatus { get; set; } = string.Empty;

        [Display(Name = "ยอดชำระก่อนตัดเป็นหนี้สูญ")]
        public decimal RcvAmtBeforeWO { get; set; }
        [Display(Name = "ยอดชำระหลังตัดเป็นหนี้สูญ")]
        public decimal RcvAmtAfterWO { get; set; }
        [Display(Name = "จำนวนเงินที่จ่ายครั้งสุดท้าย")]
        public decimal LastPaidAmount { get; set; }
        [Display(Name = "จำนวนครั้งที่ Assign")]
        public int NoOfAssignment { get; set; }
        [Display(Name = "ประเภทของสินค้า")]
        public string Description { get; set; } = string.Empty;

        [Display(Name = "ประเภทสินเชื่อ")]
        public string LoanTypeCode { get; set; } = string.Empty;
        [Display(Name = "ค่างวดตามสัญญา")]
        public decimal InstallmentsByContract { get; set; } = 0;
        [Display(Name = "ค่างวดที่ตกลง")]
        public decimal InstallmentsByAgree { get; set; } = 0;
        [Display(Name = "วันที่ชำระครั้งสุดท้าย")]
        public DateTime LastPaidDate { get; set; }
        [Display(Name = "สถานะบัญชี")]
        public int BucketId { get; set; }
        [Display(Name = "วันที่กำหนดชำระงวดแรก")]
        public DateTime FirstPaidDate { get; set; }
        [Display(Name = "อัตราดอกเบี้ยตามสัญญา")]
        public decimal IntereteRate { get; set; } = 0;
        [Display(Name = "จำนวนเงินดอกเบี้ยตามสัญญา")]
        public decimal IntereteRateAmount { get; set; } = 0;
        //[Display(Name = "ต้นเงินค้างชำระ")]
        //public decimal OverdueAmount { get; set; } = 0;
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
        [Display(Name = "OD")]
        public int OverdueDayAmount { get; set; } = 0;
        [Display(Name = "ผู้ว่าจ้าง")]
        public string EmployerCode { get; set; } = string.Empty;
        [Display(Name = "กลุ่มงาน")]
        public int LoanTaskStatusId { get; set; }

        [Display(Name = "ทีอยู่บ้าน 1")]
        public string HomeAddress1 { get; set; } = string.Empty;
        [Display(Name = "ทีอยู่บ้าน 2")]
        public string HomeAddress2 { get; set; } = string.Empty;
        [Display(Name = "ทีอยู่บ้าน 3")]
        public string HomeAddress3 { get; set; } = string.Empty;
        [Display(Name = "ทีอยู่บ้าน 4")] public string HomeAddress4 { get; set; } = string.Empty;
        [Display(Name = "ที่อยู่ออฟฟิต 1")] public string OfficeAddress1 { get; set; } = string.Empty;
        [Display(Name = "ที่อยู่ออฟฟิต 2")] public string OfficeAddress2 { get; set; } = string.Empty;
        [Display(Name = "ที่อยู่ออฟฟิต 3")] public string OfficeAddress3 { get; set; } = string.Empty;
        [Display(Name = "ที่อยู่ออฟฟิต 4")] public string OfficeAddress4 { get; set; } = string.Empty;
        [Display(Name = "เบอร์ออฟฟิต")] public string TelephoneOffice { get; set; } = string.Empty;

        [Display(Name = "ที่อยู่ตามบัตรประชาชน 1")] public string IdenAddress1 { get; set; } = string.Empty;
        [Display(Name = "ที่อยู่ตามบัตรประชาชน 2")] public string IdenAddress2 { get; set; } = string.Empty;
        [Display(Name = "ที่อยู่ตามบัตรประชาชน 3")] public string IdenAddress3 { get; set; } = string.Empty;
        [Display(Name = "ที่อยู่ตามบัตรประชาชน 4")] public string IdenAddress4 { get; set; } = string.Empty;
        [Display(Name = "เบอร์มือถือของลูกค้า")] public string MobileHome { get; set; } = string.Empty;
        [Display(Name = "เบอร์มือลูกค้าของออฟฟิต")] public string MobileOffice { get; set; } = string.Empty;
        [Display(Name = "เบอร์ฉุกเฉินของลูกค้า")] public string MobileEmg { get; set; } = string.Empty;
        [Display(Name = "ข้อมูลเพิ่มเติม")] public string SpecialNote { get; set; } = string.Empty;
        [Display(Name = "คอมเพลน Code")] public string CPCase { get; set; } = string.Empty;
        [Display(Name = "จำนวนครั้งที่คอมเพลน")]
        public int NoOfCP { get; set; }
        [Display(Name = "วันที่คอมเพลน")]
        public DateTime CPDate { get; set; }
        [Display(Name = "ค่าติดตามทวงถาม")]
        public decimal OAFee { get; set; }
        [Display(Name = "ค่าติดตามทวงถามของเงินต้น")]
        public decimal MaxOAFeeAmount { get; set; }
        [Display(Name = "ค่าติดตามทวงถามคงเหลือของเงินต้น")]
        public decimal MaxOAFeeBalance { get; set; }
        public string OAFlag { get; set; } = string.Empty;
        [Display(Name = "สถานที่ส่งเอกสาร")]
        public string SendingAddress { get; set; } = string.Empty;
    }
}
