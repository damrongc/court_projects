using System.ComponentModel.DataAnnotations;

namespace CourtJustice.Domain.ViewModels
{
    public class BankPersonCodeViewModel
    {
        public int BankPersonId { get; set; }
        public int BankActionId { get; set; }
        public string BankActionCodeId { get; set; }
        [Display(Name = "Action Code[ธนาคาร]")]
        public string BankActionCodeName { get; set; }
        [Display(Name = "Person Code[ธนาคาร]")]
        public string BankPersonCodeId { get; set; }
        [Display(Name = "คำอธิบาย")]
        public string BankPersonCodeName { get; set; }
        [Display(Name = "สถานะ")]
        public bool IsActive { get; set; } = true;
        public List<BankResultCodeViewModel> BankResultCodes { get; set; } = new();
        public int BankResultCodeCount { get; set; }
    }
}
