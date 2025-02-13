﻿using System;
using System.ComponentModel.DataAnnotations;

namespace BloodDonationAPI.DTOs.Admin
{
    public class UpdateUserDto
    {
        [StringLength(50)]
        public string? Username { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [StringLength(3)]
        public string? BloodType { get; set; } // e.g., A+, O-, etc.

        public DateTime? DateOfBirth { get; set; }

        [StringLength(10)]
        public string? Gender { get; set; } // e.g., Male, Female, Other

        [Phone]
        public string? PhoneNumber { get; set; }

        [StringLength(200)]
        public string? Address { get; set; }

        [StringLength(20)]
        public string? Role { get; set; } // "admin" or "user"

        [MinLength(6)]
        public string? NewPassword { get; set; } // Optional: For updating password
    }
}
