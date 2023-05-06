using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtJustice.Domain.Models
{
    [Table("user_permission")]
    public class UserPermission : BaseEntity
    {
        [Key]
        public int PermissionId { get; set; }
        public int GroupId { get; set; }
        public int ProgramId { get; set; }

        [ForeignKey(nameof(GroupId))]
        public virtual GroupUser GroupUser { get; private set; }

        [ForeignKey(nameof(ProgramId))]
        public virtual AppProgram AppProgram { get; private set; }
    }
}
