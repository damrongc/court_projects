﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourtJustice.Domain.Models
{
    [Table("company")]
    public class Company : BaseEntity
    {
        [Display(Name = "รหัสบริษัท")]
        public int CompanyId { get; set; }
        [Display(Name = "บริษัท")]
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string LogoPath { get; set; }

    }
}
