using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtJustice.Domain.Models
{
    [Table("bank_person_code")]
    public class BankPersonCode
    {
        [Key]
        public int BankPersonId { get; set; }
        public string BankPersonCodeId { get; set; }
        public string BankPersonCodeName { get; set; }
        public int BankActionId { get; set; }
    }
}
