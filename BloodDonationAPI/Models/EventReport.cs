// File: BloodDonationAPI/Models/EventReport.cs

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BloodDonationAPI.Models
{
    public class EventReport
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int EventId { get; set; }

        [Required]
        public int NumberOfDonors { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Cost { get; set; }

        [StringLength(1000)]
        public string Notes { get; set; }

        // Navigation Properties
        public Event Event { get; set; }

        public ICollection<BloodVariantReport> BloodVariantReports { get; set; }
    }
}
