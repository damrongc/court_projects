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
    [Table("law_case")]
    public class LawCase :BaseEntity
    {
        [Key]
        [Display(Name = "รหัสคดี")]
        public string LawCaseCode { get; set; } = string.Empty;  //คดีผู้บริโภค ,คดีแพ่ง
        [Display(Name = "คดี")]
        public string LawCaseName { get; set; } = string.Empty;
    }
}
