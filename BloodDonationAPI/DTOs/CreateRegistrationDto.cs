// File: BloodDonationAPI/DTOs/CreateRegistrationDto.cs

using System.ComponentModel.DataAnnotations;

namespace BloodDonationAPI.DTOs
{
    public class CreateRegistrationDto
    {
        [Required]
        public int EventId { get; set; }
    }
}
