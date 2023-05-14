using System;
using CourtJustice.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace CourtJustice.Domain.ViewModels
{
	public class AssetCarViewModel
	{
    
        [Display(Name = "เลขที่ตัวถัง")]
        public string ChassisNumber { get; set; } = string.Empty;

        [Display(Name = "เลขเครื่องยนต์")]
        public string EngineNumber { get; set; } = string.Empty;

        [Display(Name = "ยี่ห้อ")]
        public string Brand { get; set; } = string.Empty;

        [Display(Name = "รุ่น/แบบ")]
        public string Model { get; set; } = string.Empty;

        [Display(Name = "ปี")]
        public int ProductionYear { get; set; }
        [Display(Name = "ราคาประเมิน")]
        public decimal EstimatePrice { get; set; } = 0;

        [Display(Name = "ทะเบียนรถ")]
        public string LicensePlate { get; set; } = string.Empty;

        [Display(Name = "เจ้าของกรรมสิทธิ")]
        public string Owner { get; set; } = string.Empty;

        [Display(Name = "ประเภทรถ")]
        public int CarTypeCode { get; set; }
        [ForeignKey(nameof(CarTypeCode))]
        public virtual CarType? CarType { set; get; }

        [Display(Name = "ลูกหนี้")]
        public string CusId { get; set; }
        [ForeignKey(nameof(CusId))]
        public virtual Loanee? Loanee { set; get; }
    }
}

