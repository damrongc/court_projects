using System.ComponentModel.DataAnnotations;

namespace CourtJustice.Domain.ViewModels
{
    public class JusticeCaseLawyerViewModel
    {
        public int JusticeCaseLawyerId { get; set; }
        public int LawyerId { get; set; }
        [Display(Name = "ทนาย")]
        public string LawyerName { get; set; }=string.Empty;
    }
}
