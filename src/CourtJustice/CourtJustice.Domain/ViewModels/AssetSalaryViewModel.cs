﻿using System;
using CourtJustice.Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace CourtJustice.Domain.ViewModels
{
	public class AssetSalaryViewModel
	{

        [Display(Name = "รหัสสินทรัพย์")]
        public int AssetId { get; set; }
        public string Company { get; set; } = string.Empty;

        public decimal Salary { get; set; } = 0;
        public DateOnly SalaryDate { get; set; }

        public string Address { get; set; } = string.Empty;
        [Display(Name = "รายละเอียด(ตำบล,อำเภอ,จังหวัด,รหัสไปรษณีย์)")]
        public string AddressDetail { get; set; } = string.Empty;
        public string CusId { get; set; }
     

    }
}

