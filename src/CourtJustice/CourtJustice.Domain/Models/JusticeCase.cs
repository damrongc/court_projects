
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtJustice.Domain.Models
{
    [Table("justice_case")]
    public class JusticeCase
    {
        [Key]
        public string BlackCaseNo { get; set; }
        public DateOnly? CaseDate { get; set; }
        public DateOnly? ApprovalDate { get; set; }
        public DateOnly? JudgmentDate { get; set; }
        public decimal AssetAmount { get; set; } = 0;
        public string CaseDocumentResult { get; set; } = string.Empty;
        public decimal FeeCase { get; set; } = 0;
        public DateOnly? SubmissionDate { get; set; }
        public string SubmissionResult { get; set; }=string.Empty;
        public DateOnly? CommitDate { get; set; }
        public DateOnly? PostingDate { get; set; }
        
        public int CaseResultId { get; set; }
        [ForeignKey(nameof(CaseResultId))]
        public virtual CaseResult? CaseResult { set; get; }

        public string CourtId { get; set; }
        [ForeignKey(nameof(CourtId))]
        public virtual Court? Court { set; get; }

        public string CusId { get; set; } = string.Empty;

    }
}
