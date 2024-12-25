// File: BloodDonationClient/Models/CreateRegistrationDto.cs

using System.ComponentModel.DataAnnotations;

namespace BloodDonationClient.Models
{
    public class CreateRegistrationDto
    {
        [Required]
        public int EventId { get; set; }
    }
}
