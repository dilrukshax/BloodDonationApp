// File: BloodDonationAPI/DTOs/EventCreateDto.cs

using System;
using System.ComponentModel.DataAnnotations;

namespace BloodDonationAPI.DTOs
{
    public class EventCreateDto
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public DateTime EventDate { get; set; }

        [Required]
        [StringLength(200)]
        public string Location { get; set; }
    }
}
