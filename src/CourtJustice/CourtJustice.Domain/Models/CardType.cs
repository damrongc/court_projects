using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CourtJustice.Domain.Models
{
    [Table("card_type")]
    public class CardType
    {
        [Key]
        [Display(Name = "รหัสประเภทบัตร")]
        public string CardTypeCode { get; set; } = string.Empty;
        [Required]
        [Display(Name = "ประเภทบัตร")]
        public string CardTypeName { get; set; } = string.Empty;
    }
}
