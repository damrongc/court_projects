using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtJustice.Domain.Models
{
    [Table("justice_case_document")]
    public class JusticeCaseDocument
    {
        [Key]
        public int JusticeCaseDocumentId { get; set; }
        [Required]
        public string DocumentName { get; set; }
        public string DocumentType { get; set; }
        public string DocumentPath { get; set; }
        public string BlackCaseNo { get; set; }
        [ForeignKey(nameof(BlackCaseNo))]
        public virtual JusticeCase? JusticeCase { set; get; }
    }
}
