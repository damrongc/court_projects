using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourtJustice.Domain.Models
{
    [Table("justice_appointment")]
    public class JusticeAppointment
    {
        [Key]
        public int JusticeAppointmentId { get; set; }
        [Required]
        public DateOnly AppointmentDate { get; set; }
        [Required]
        public string Remark { get; set; }
        [Required]
        public string BlackCaseNo { get; set; }
        [ForeignKey(nameof(BlackCaseNo))]
        public virtual JusticeCase? JusticeCase { set; get; }
    }
}
