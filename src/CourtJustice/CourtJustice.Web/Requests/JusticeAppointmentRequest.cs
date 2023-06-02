using System.ComponentModel.DataAnnotations;

namespace CourtJustice.Web.Requests
{
    public class JusticeAppointmentRequest
    {
        public string AppointmentDate { get; set; }
        public string Remark { get; set; }
    }
}
