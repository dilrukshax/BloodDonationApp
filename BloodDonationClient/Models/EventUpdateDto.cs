// File: BloodDonationClient/Models/EventUpdateDto.cs

using System;
using System.ComponentModel.DataAnnotations;

namespace BloodDonationClient.Models
{
    public class EventUpdateDto
    {
        [StringLength(100, ErrorMessage = "Title cannot exceed 100 characters.")]
        public string Title { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }

        public DateTime? EventDate { get; set; }

        [StringLength(200, ErrorMessage = "Location cannot exceed 200 characters.")]
        public string Location { get; set; }
    }
}
