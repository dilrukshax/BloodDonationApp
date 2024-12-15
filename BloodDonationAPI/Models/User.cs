// File: BloodDonationAPI/Models/User.cs

using System;
using System.ComponentModel.DataAnnotations;

namespace BloodDonationAPI.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        // Profile Fields
        [Required]
        [StringLength(3)]
        public string BloodType { get; set; } // e.g., A+, O-, etc.

        [Required]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(10)]
        public string Gender { get; set; } // e.g., Male, Female, Other

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [StringLength(200)]
        public string Address { get; set; }
    }
}
