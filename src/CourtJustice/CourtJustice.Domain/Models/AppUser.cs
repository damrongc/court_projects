using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtJustice.Domain.Models
{
    [Table("app_user")]
    public class AppUser : BaseEntity
    {
        [Key]
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int GroupId { get; set; }

        [ForeignKey(nameof(GroupId))]
        public virtual GroupUser GroupUser { get; private set; }

    }
}
