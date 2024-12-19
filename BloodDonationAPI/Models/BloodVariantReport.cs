// File: BloodDonationAPI/Models/BloodVariantReport.cs

using System.ComponentModel.DataAnnotations;

namespace BloodDonationAPI.Models
{
    public class BloodVariantReport
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int EventReportId { get; set; }

        [Required]
        [StringLength(3)]
        public string BloodType { get; set; } // e.g., A+, O-, etc.

        [Required]
        public int UnitsCollected { get; set; }

        // Navigation Property
        public EventReport EventReport { get; set; }
    }
}
