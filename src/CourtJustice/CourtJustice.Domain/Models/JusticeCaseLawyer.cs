using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtJustice.Domain.Models
{
    [Table("justice_case_lawyer")]
    public class JusticeCaseLawyer
    {
        [Key]
        public int JusticeCaseLawyerId { get; set; }
        public string LawyerCode { get; set; }
        [ForeignKey(nameof(LawyerCode))]
        public virtual Lawyer? Lawyer { set; get; }

        [Required]
        public string BlackCaseNo { get; set; }
        [ForeignKey(nameof(BlackCaseNo))]
        public virtual JusticeCase? JusticeCase { set; get; }
    }
}
