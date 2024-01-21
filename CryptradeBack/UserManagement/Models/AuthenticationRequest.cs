using System;
namespace UserManagement.Models
{
	public class AuthenticationRequest
	{
		public string email { get; set; }
		public string password { get; set; }
	}
}