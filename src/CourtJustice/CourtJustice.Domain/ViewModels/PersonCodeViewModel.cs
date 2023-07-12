using System.ComponentModel.DataAnnotations;

namespace CourtJustice.Domain.ViewModels
{
    public class PersonCodeViewModel
    {
        public string PersonCodeId { get; set; }
        [Display(Name = "Person Code[ธนาคาร]")]
        public string PersonCodeCode { get; set; }

        [Display(Name = "คำอธิบาย")]
        public string PersonCodeDescription { get; set; }
        [Display(Name = "Bank Action")]
        public string BankActionCodeId { get; set; }
        //[Display(Name = "ผู้ว่าจ้าง")]
        //public string EmployerCode { get; set; }
    }
}
