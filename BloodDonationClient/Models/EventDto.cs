// File: BloodDonationClient/Models/EventDto.cs

using System;

namespace BloodDonationClient.Models
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
