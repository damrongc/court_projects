using System;
using System.ComponentModel.DataAnnotations;

namespace CourtJustice.Domain.ViewModels
{
	public class BankResultCodeViewModel
	{
        public int BankResultId { get; set; }
        [Display(Name = "Result Code[ธนาคาร]")]
        public string BankResultCodeId { get; set; }
        [Display(Name = "Result Name[ธนาคาร]")]
        public string BankResultCodeName { get; set; }
        public string EmployerCode { get; set; }
        [Display(Name = "ผู้ว่าจ้าง/ธนาคาร")]
        public string EmployerName { get; set; }
    }
}

