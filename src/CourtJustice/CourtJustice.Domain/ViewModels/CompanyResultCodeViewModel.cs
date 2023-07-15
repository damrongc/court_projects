using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CourtJustice.Domain.ViewModels
{
	public class CompanyResultCodeViewModel
	{
        public int CompanyResultId { get; set; }
        [Display(Name = "Result Code[บริษัท]")]
        public string CompanyResultCodeId { get; set; }
        [Display(Name = "คำอธิบาย")]
        public string CompanyResultCodeName { get; set; }
        public bool NotCallFlag { get; set; }
        public bool ShowHideFlag { get; set; }
        [Display(Name = "บริษัท")]
        public int CompanyId { get; set; }
        public int CompanyActionId { get; set; }
    }
}

