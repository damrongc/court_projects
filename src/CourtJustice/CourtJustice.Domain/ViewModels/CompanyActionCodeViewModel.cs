using System;
using System.ComponentModel.DataAnnotations;

namespace CourtJustice.Domain.ViewModels
{
	public class CompanyActionCodeViewModel
	{
        public int CompanyActionId { get; set; }

        [Display(Name = "Action Code[บริษัท]")]
        public string CompanyActionCodeId { get; set; }

        [Display(Name = "คำอธิบาย")]
        public string CompanyActionCodeName { get; set; }

        [Display(Name = "บริษัท")]
        public int CompanyId { get; set; }

        [Display(Name = "Result Code[บริษัท]")]
        public List<CompanyResultCodeViewModel> CompanyResultCodes { get; set; } = new();
    }
}

