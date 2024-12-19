// File: BloodDonationAPI/DTOs/EventDto.cs

using System;

namespace BloodDonationAPI.DTOs
{
    public class EventDto
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime EventDate { get; set; }

        public string Location { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
