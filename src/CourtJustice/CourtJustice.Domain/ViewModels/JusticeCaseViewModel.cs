using System.ComponentModel.DataAnnotations;

namespace CourtJustice.Domain.ViewModels
{
    public class JusticeCaseViewModel
    {
        [Display(Name = "เลขคดีดำ")]
        public string BlackCaseNo { get; set; } = string.Empty;
        [Display(Name = "วันฟ้อง")]
        public DateTime CaseDate { get; set; }
        [Display(Name = "วันอนุมัติฟ้อง")]
        public DateTime ApprovalDate { get; set; }
        [Display(Name = "วันพิพากษา")]
        public DateTime JudgmentDate { get; set; }
        [Display(Name = "ทุนทรัพย์")]
        public decimal AssetAmount { get; set; } = 0;
        [Display(Name = "ผลการส่งหมายเรียก")]
        public string CaseDocumentResult { get; set; } = string.Empty;
        [Display(Name = "ค่านำหมาย")]
        public decimal FeeCase { get; set; } = 0;
        [Display(Name = "วันที่วางเงินค่าส่งคำบังคับ")]
        public DateTime SubmissionDate { get; set; }
        [Display(Name = "ผลการส่งคำบังคับ")]
        public string SubmissionResult { get; set; } = string.Empty;
        [Display(Name = "วันที่ครบกำหนดออกหมายตั้ง")]
        public DateTime CommitDate { get; set; }
        [Display(Name = "วันที่ยื่นออกหมายตั้ง")]
        public DateTime PostingDate { get; set; }

        public string CourtId { get; set; }
        [Display(Name = "ศาล")]
        public string CourtName { get; set; } = string.Empty;

        public int CaseResultId { get; set; }
        [Display(Name = "ผลคดี")]
        public string CaseResultName { get; set; } = string.Empty;
        [Display(Name = "เลขที่ผู้เช่าซื้อ")]
        public string CusId { get; set; } = string.Empty;
        [Display(Name = "ชื่อ-นามสุกล")]
        public string CusName { get; set; } = string.Empty;

        public List<JusticeAppointmentViewModel> JusticeAppointments { get; set; } = new();
        public List<JusticeCaseLawyerViewModel> JusticeCaseLawyers { get; set; } = new();

    }
}
