// File: BloodDonationAPI/DTOs/EventUpdateDto.cs

using System;
using System.ComponentModel.DataAnnotations;

namespace BloodDonationAPI.DTOs
{
    public class EventUpdateDto
    {
        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public DateTime? EventDate { get; set; }

        [StringLength(200)]
        public string Location { get; set; }
    }
}
