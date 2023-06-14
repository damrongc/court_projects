using CourtJustice.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace CourtJustice.Domain.ViewModels
{
    public class LoaneeRemarkViewModel
	{
        public int LoaneeRemarkId { get; set; }
        public string Remark { get; set; }
        public string CusId { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy HH:mm}")]
        public DateTime TransactionDatetime { get; set; } = DateTime.Now;
        public string BankActionCodeId { get; set; }
        public string BankActionCodeName { get; set; }
        public string BankResultCodeId { get; set; }
        public string BankResultCodeName { get; set; }
        public string CompanyActionCodeId { get; set; }
        public string CompanyActionCodeName { get; set; }
        public string CompanyResultCodeId { get; set; }
        public string CompanyResultCodeName { get; set; }

        [Display(Name = "เบอร์ติดต่อ")]
        public string ContractNo { get; set; }

        [Required]
        [Display(Name = "วันนัด")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateOnly AppointmentDate { get; set; }

        [Display(Name = "ยอดจ่าย")]
        [DisplayFormat(DataFormatString = "{0:###,###.00}")]
        public decimal Amount { get; set; }

        [Display(Name = "ผู้นัด")]
        public string AppointmentContract { get; set; }
    }
}

