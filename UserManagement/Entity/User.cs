using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserManagement.Entity
{
    [Table("Users")]
	public class User
	{
        [Key]
        [Column("id")]
		public int id { get; set; }

        [Column("name")]
        public string name { get; set; }

        [Column("email")]
        public string email { get; set; }

        [Column("password")]
        public string password { get; set; }

        [Column("role")]
        public string role { get; set; }
    }
}