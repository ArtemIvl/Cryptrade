﻿using System;
using System.ComponentModel.DataAnnotations;

namespace UserManagement.Models
{
    public class UserRegisterModel
    {
        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Name has to be at least 3 and at max 20 characters")]
        public string name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(40, MinimumLength = 8, ErrorMessage = "Email has to be at least 8 and at max 40 characters")]
        public string email { get; set; }

        [Required]
        [StringLength(25, MinimumLength = 6, ErrorMessage = "Password has to be at least 6 and at max 25 characters")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Required]
        [Compare("password", ErrorMessage = "Passwords do not match")]
        public string confirmPassword { get; set; }
    }
}

