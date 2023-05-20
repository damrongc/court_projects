using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtJustice.Domain.Models
{
    [Table("credit_type")]
    public class CreditType : BaseEntity
    {
        [Key]
        [Display(Name = "รหัสประเภทสินเชื่อ")]
        public string CreditTypeCode { get; set; } = string.Empty;
        [Required]
        [Display(Name = "ประเภทสินเชื่อ")]
        public string CreditTypeName { get; set; }=string.Empty;
    }
}
