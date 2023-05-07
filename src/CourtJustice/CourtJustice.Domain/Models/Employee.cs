using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtJustice.Domain.Models
{
    [Table("employee")]
    public class Employee : BaseEntity
    {
        [Key]
        [Display(Name = "รหัสพนักงาน")]
        public string EmployeeCode { get; set; } = string.Empty;
        [Required]
        [Display(Name = "พนักงาน")]
        public string EmployeeName { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        [Display(Name = "เบอร์ติดต่อ")]
        public string PhoneNumber { get; set; } = string.Empty;
        [Display(Name = "วันที่จ้าง")]
        public DateOnly HireDate { get; set; }
        [Display(Name = "เป้าหมาย")]
        public decimal Target { get; set; } = 0;
        [Display(Name = "ที่อยู่")]
        public string Address { get; set; } = string.Empty;
        public int AddressId { get; set; }
        [ForeignKey(nameof(AddressId))]
        public virtual AddressSet? AddressSet { set; get; }

    }
}
