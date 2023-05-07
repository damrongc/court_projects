using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtJustice.Domain.Models
{
    [Table("referencer")]
    public class Referencer
    {
        [Key]
        [Display(Name = "เลขบัตร ปชช")]
        public string ReferencerCode { get; set; } = string.Empty;
        [Display(Name = "บุคคลอ้างอิง")]
        public string FullName { get; set; } = string.Empty;
        [Display(Name = "เบอร์ติดต่อ")]
        public string PhoneNumber { get; set; } = string.Empty;
        [Display(Name = "ที่อยู่")]
        public string Address { get; set; } = string.Empty;
        
    }
}
