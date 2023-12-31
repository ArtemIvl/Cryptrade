﻿using System;
using System.ComponentModel.DataAnnotations;

namespace UserManagement.Models
{
	public class UserLoginModel
	{
        [Required]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}

