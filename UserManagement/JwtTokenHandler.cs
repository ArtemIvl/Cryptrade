using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using UserManagement.Entity;
using UserManagement.Models;
using Microsoft.IdentityModel.Tokens;

namespace UserManagement
{
	public class JwtTokenHandler
	{
		public const string JWT_SECURITY_KEY = "yPRCqn4kSWLtaJwXvN2jGzpQRyTZ3gdXkt7FeBJP";

		private const int JWT_TOKEN_VALIDITY_MINS = 20;

        public JwtTokenHandler()
        {
			
        }

        public AuthenticationResponse? GenerateJwtToken(AuthenticationRequest authenticationRequest, List<User> users)
		{
			if (string.IsNullOrWhiteSpace(authenticationRequest.email) || string.IsNullOrWhiteSpace(authenticationRequest.password))
				return null;

			// Validate
			var userAccount = users.FirstOrDefault(u => u.email == authenticationRequest.email);

			if (userAccount == null || !BCrypt.Net.BCrypt.Verify(authenticationRequest.password, userAccount.password))
			{
				return null;
			}

			var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(JWT_TOKEN_VALIDITY_MINS);
			var tokenKey = Encoding.ASCII.GetBytes(JWT_SECURITY_KEY);
			var claimsIdentity = new ClaimsIdentity(new List<Claim>
			{
				new Claim(JwtRegisteredClaimNames.Email, authenticationRequest.email),
				new Claim(JwtRegisteredClaimNames.Name, userAccount.name),
				new Claim(ClaimTypes.NameIdentifier, userAccount.id.ToString()), // User's unique identifier
				new Claim(ClaimTypes.Role, userAccount.role),
				// new Claim("Role", userAccount.role) - for ocelot use
			});

			var signingCredentials = new SigningCredentials(
				new SymmetricSecurityKey(tokenKey),
				SecurityAlgorithms.HmacSha256Signature);

			var securityTokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = claimsIdentity,
				Expires = tokenExpiryTimeStamp,
				SigningCredentials = signingCredentials
			};

			var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
			var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
			var token = jwtSecurityTokenHandler.WriteToken(securityToken);

			return new AuthenticationResponse
			{
				email = userAccount.email,
				expiresIn = (int)tokenExpiryTimeStamp.Subtract(DateTime.Now).TotalSeconds,
				jwtToken = token
			};
		}
    }
}

