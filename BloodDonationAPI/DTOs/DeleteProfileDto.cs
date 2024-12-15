// File: BloodDonationAPI/DTOs/DeleteProfileDto.cs

using System.ComponentModel.DataAnnotations;

namespace BloodDonationAPI.DTOs
{
    public class DeleteProfileDto
    {
        [Required]
        public string Password { get; set; }
    }
}
