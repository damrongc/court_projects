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

        [Display(Name = "วันที่ทำ")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime TransactionDatetime { get; set; } = DateTime.Now;


        [Display(Name = "รหัสการดำเนินการของธนาคาร")]
        public string BankActionCodeId { get; set; }

        [Display(Name = "การดำเนินการของธนาคาร")]
        public string BankActionCodeName { get; set; }
        [Display(Name = "รหัสผลการดำเนินการของธนาคาร")]
        public string BankResultCodeId { get; set; }

        [Display(Name = "ผลการดำเนินการของธนาคาร")]
        public string BankResultCodeName { get; set; }
        [Display(Name = "รหัสการดำเนินการของบริษัท")]
        public string CompanyActionCodeId { get; set; }

        [Display(Name = "การดำเนินการของบริษัท")]
        public string CompanyActionCodeName { get; set; }
        [Display(Name = "รหัสผลการดำเนินการของบริษัท")]
        public string CompanyResultCodeId { get; set; }

        [Display(Name = "ผลการดำเนินการของบริษัท")]
        public string CompanyResultCodeName { get; set; }

        [Display(Name = "เบอร์ติดต่อ")]
        public string ContractNo { get; set; }

      
        [Display(Name = "วันนัด")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime AppointmentDate { get; set; }

        [Display(Name = "ยอดจ่าย")]
        [DisplayFormat(DataFormatString = "{0:###,###.00}")]
        public decimal Amount { get; set; }

        [Display(Name = "ผู้นัด")]
        public string AppointmentContract { get; set; }
    }
}

