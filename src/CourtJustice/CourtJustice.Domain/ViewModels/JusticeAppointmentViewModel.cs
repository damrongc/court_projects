using System.ComponentModel.DataAnnotations;

namespace CourtJustice.Domain.ViewModels
{
    public class JusticeAppointmentViewModel
    {
        public int JusticeAppointmentId { get; set; }
        [Display(Name = "วันนัดพิจารณา")]
        public DateTime AppointmentDate { get; set; }
        [Display(Name = "หมายเหตุ")]
        public string Remark { get; set; }
        public string BlackCaseNo { get; set; }
    }
}
