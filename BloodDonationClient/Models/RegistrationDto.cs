// File: BloodDonationClient/Models/RegistrationDto.cs

using System;

namespace BloodDonationClient.Models
{
    public class RegistrationDto
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public string EventTitle { get; set; }
        public DateTime EventDate { get; set; }
        public string EventLocation { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public string UserEmail { get; set; }
        public DateTime RegisteredAt { get; set; }
    }
}
