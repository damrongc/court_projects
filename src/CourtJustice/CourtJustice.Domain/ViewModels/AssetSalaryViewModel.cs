﻿using System;
using CourtJustice.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace CourtJustice.Domain.ViewModels
{
	public class AssetSalaryViewModel
	{

        [Display(Name = "โฉนดเลขที่")]
        public string AssetLandId { get; set; } = string.Empty;
        [Display(Name = "ระวาง")]
        public string Position { get; set; } = string.Empty;
        [Display(Name = "ที่ตั้ง")]
        public string Address { get; set; } = string.Empty;
        [Display(Name = "รายละเอียด(ตำบล,อำเภอ,จังหวัด,รหัสไปรษณีย์)")]
        public string AddressDetail { get; set; } = string.Empty;
        public string Gps { get; set; } = string.Empty;
        [Display(Name = "สำนักงานที่ดิน")]
        public string LandOfficeCode { get; set; } = string.Empty;
        [Display(Name = "ราคาประเมิน")]
        public decimal EstimatePrice { get; set; } = 0;
        [ForeignKey(nameof(LandOfficeCode))]
        public virtual LandOffice? LandOffice { set; get; }
        [Display(Name = "ลูกหนี้")]
        public string CusId { get; set; }
        [ForeignKey(nameof(CusId))]
        public virtual Loanee? Loanee { set; get; }
    }
}

