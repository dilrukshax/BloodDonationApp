// File: BloodDonationAPI/Models/EventReport.cs

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BloodDonationAPI.Models
{
    public class EventReport
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int EventId { get; set; }

        [ForeignKey("EventId")]
        public Event Event { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Participated people count must be non-negative.")]
        public int ParticipatedPeopleCount { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Cost must be non-negative.")]
        public decimal Cost { get; set; }

        // Blood collection by type
        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "A+ blood count must be non-negative.")]
        public int APositive { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "B+ blood count must be non-negative.")]
        public int BPositive { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "AB+ blood count must be non-negative.")]
        public int ABPositive { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "O+ blood count must be non-negative.")]
        public int OPositive { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "A- blood count must be non-negative.")]
        public int ANegative { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "B- blood count must be non-negative.")]
        public int BNegative { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "AB- blood count must be non-negative.")]
        public int ABNegative { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "O- blood count must be non-negative.")]
        public int ONegative { get; set; }
    }
}
