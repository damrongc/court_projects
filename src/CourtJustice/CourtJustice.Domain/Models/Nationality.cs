using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtJustice.Domain.Models
{
    [Table("nationality")]
    public class Nationality : BaseEntity
    {
        [Key]
        [Display(Name = "รหัสสัญชาติ")]
        public string NationalityCode { get; set; } =string.Empty;
        [Display(Name = "สัญชาติ")]
        public string NationalityName { get; set; } = string.Empty;
    }
}
