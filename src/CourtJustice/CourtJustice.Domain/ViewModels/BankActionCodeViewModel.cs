using System.ComponentModel.DataAnnotations;

namespace CourtJustice.Domain.ViewModels
{
	public class BankActionCodeViewModel
	{
        public int BankActionId { get; set; }
        [Display(Name = "Action Code[ธนาคาร]")]
        public string BankActionCodeId { get; set; }
        [Display(Name = "Action Name[ธนาคาร]")]
        public string BankActionCodeName { get; set; }
        public string EmployerCode { get; set; }
        [Display(Name = "ผู้ว่าจ้าง/ธนาคาร")]
        public string EmployerName { get; set; }
        [Display(Name = "Person Code[ธนาคาร]")]
        public List<BankPersonCodeViewModel> BankPersonCodes { get; set; }=new();

    }
}

