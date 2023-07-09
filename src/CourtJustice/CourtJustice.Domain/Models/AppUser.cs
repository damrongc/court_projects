using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtJustice.Domain.Models
{
    [Table("app_user")]
    public class AppUser : BaseEntity
    {
        [Key]
        [Display(Name = "รหัสผู้ใช้")]
        public string UserId { get; set; }
        [Display(Name = "ชื่อผู้ใช้")]
        public string UserName { get; set; }
        [Display(Name = "รหัสผ่าน")]
        public string Password { get; set; }
        [Display(Name = "เมลล์")]
        public string Email { get; set; }
        [Display(Name = "เบอร์ติดต่อ")]
        public string PhoneNumber { get; set; }
        public int GroupId { get; set; }
        [ForeignKey(nameof(GroupId))]
        public virtual GroupUser? GroupUser { get; private set; }

        public string? ManagerId { get; set; }
        public decimal Target { get; set; } = 0;

    }
}
