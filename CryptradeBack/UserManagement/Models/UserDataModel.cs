using System;
using System.ComponentModel.DataAnnotations;

namespace UserManagement.Models
{
	public class UserDataModel
	{
        [Required]
        public string name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        public int id { get; set; }
    }
}

