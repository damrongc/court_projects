using System.ComponentModel.DataAnnotations;

namespace CourtJustice.Domain.ViewModels
{
    public class JusticeCaseDocumentViewModel
    {
        public int JusticeCaseDocumentId { get; set; }
        [Display(Name = "ชื่อเอกสาร")]
        public string DocumentName { get; set; }
        [Display(Name = "ประเภทเอกสาร")]
        public string DocumentType { get; set; }
        public string DocumentPath { get; set; }
        public string BlackCaseNo { get; set; }
    }
}
