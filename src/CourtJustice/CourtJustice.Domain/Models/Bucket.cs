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
    [Table("bucket")]
    public class Bucket
    {
        [Key]
        [Display(Name = "รหัสสถานะบัญชี")]
        public int BucketId { get; set; }
        [Display(Name = "สถานะบัญชี")]
        public string BucketName { get; set; } = string.Empty;
    }
}
