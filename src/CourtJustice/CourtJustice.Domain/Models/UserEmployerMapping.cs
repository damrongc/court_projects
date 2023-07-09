using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtJustice.Domain.Models
{
    [Table("user_employer_mapping")]
    public class UserEmployerMapping
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public string EmployerCode { get; set; }
    }
}
