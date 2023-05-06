using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtJustice.Domain.Models
{
    [Table("case_result")]
    public class CaseResult
    {
        [Key]
        [Display(Name = "รหัสผลคดี")]
        public int CaseResultId { get; set; }

        [Display(Name = "ผลคดี")]
        public string CaseResultName { get; set; } = string.Empty; //เลื่อน / พิพากษา / ทำยอม / ถอนฟ้อง 
    }
}
