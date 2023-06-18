using System.ComponentModel.DataAnnotations;

namespace CourtJustice.Domain.ViewModels
{
    public class AppUserViewModel
    {
        public string UserId { get; set; }
        public string UserName { get; set;}
        public int  GroupId { get; set; }
        public string GroupName { get; set; }

        [Required]
        [Display(Name = "รหัสผ่าน")]
        public string Password { get; set; }

        [Display(Name = "รหัสผ่านใหม่")]
        public string NewPassword { get; set; }

        [Display(Name = "ยืนยันรหัสผ่าน")]
        public string ConfirmPassword { get; set; }

        public bool SitePermission { get; set; }
    }
}
