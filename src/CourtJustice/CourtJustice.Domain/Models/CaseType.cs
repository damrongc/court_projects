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
    [Table("case_type")]
    public class CaseType :BaseEntity
    {

        [Key]
        [Display(Name = "รหัสประเภทคดี")]
        public string CaseTypeCode { get; set; } = string.Empty;
        [Display(Name = "ประเภทคดี")]
        public string CaseTypeName { get; set; } = string.Empty;
    }
}
