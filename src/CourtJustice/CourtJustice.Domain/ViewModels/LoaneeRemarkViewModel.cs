using System.ComponentModel.DataAnnotations;

namespace CourtJustice.Domain.ViewModels
{
    public class LoaneeRemarkViewModel
	{
        public int LoaneeRemarkId { get; set; }
        public string ContractNo { get; set; }
        public string NationalityId { get; set; }
        public string CusId { get; set; }
        public string Name { get; set; }

        [Display(Name = "วันเวลาทำรายการ")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy HH:mm}")]
        public DateTime TransactionDatetime { get; set; } = DateTime.Now;
        
        public string BankActionCodeId { get; set; }
        [Display(Name = "Action Code[ธนาคาร]")]public string BankActionCodeName { get; set; }

        public string BankResultCodeId { get; set; }
        [Display(Name = "Result Code[ธนาคาร]")]public string BankResultCodeName { get; set; }

        //public string PersonCodeId { get; set; }
        //public string PersonCodeCode { get; set; }
        //[Display(Name = "Person Code[ธนาคาร]")]public string PersonCodeDescription { get; set; }
        
        public string CompanyActionCodeId { get; set; }
        [Display(Name = "Action Code[บริษัท]")]public string CompanyActionCodeName { get; set; }
        public string CompanyResultCodeId { get; set; }

        [Display(Name = "Result Code[บริษัท]")]public string CompanyResultCodeName { get; set; }

        [Display(Name = "เบอร์ติดต่อ")]
        public string FollowContractNo { get; set; }

        [Display(Name = "วันนัด")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        public DateTime AppointmentDate { get; set; }

        [Display(Name = "ยอดจ่าย")]
        [DisplayFormat(DataFormatString = "{0:###,###.00}")]
        public decimal Amount { get; set; }
        [Display(Name = "ผู้นัด")]
        public string AppointmentContract { get; set; }
        [Display(Name = "หมายเหตุ")]
        public string Remark { get; set; }
        [Display(Name = "ผู้ว่าจ้าง/ธนาคาร")]
        public string EmployerCode { get; set; }
        [Display(Name = "Person Code[ธนาคาร]")]
        public string BankPersonCodeId { get; set; }
        [Display(Name = "Person Code[ธนาคาร]")]
        public string BankPersonCodeName { get; set; }
        public int BankActionId { get; set; }
        public int BankResultId { get; set; }
        public int BankPersonId { get; set; }
        public int CompanyActionId { get; set; }
        public int CompanyResultId { get; set; }
        public bool EmployeeChecked { get; set; }
        public bool ManagerChecked { get; set; }
    }
}

