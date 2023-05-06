using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtJustice.Domain.Models
{
    [Table("group_user")]
    public class GroupUser : BaseEntity
    {
        [Key]
        [Display(Name = "รหัสกลุ่มผู้ใช้")]
        public int GroupId { get; set; }
        [Required]
        [Display(Name = "กลุ่มผู้ใช้")]
        public string GroupName { get; set; }
        public virtual ICollection<AppUser> AppUsers { get; set; }
    }
}
