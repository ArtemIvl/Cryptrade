using System;
namespace JwtAuthenticationManager.Models
{
	public class AuthenticationResponse
	{
		public string email { get; set; }
		public string jwtToken { get; set; }
		public int expiresIn { get; set; }
	}
}

