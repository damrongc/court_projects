using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtJustice.Domain.Models
{
    [Table("loan_type")]
    public class LoanType : BaseEntity
    {
        [Key]
        [Display(Name = "รหัสประเภทสินเชื่อ")]
        public string LoanTypeCode { get; set; } = string.Empty;
        [Display(Name = "ประเภทสินเชื่อ")]
        public string LoanTypeName { get; set; } = string.Empty;
    }
}
