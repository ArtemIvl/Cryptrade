using System;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserManagement.Data;
using UserManagement.Entity;
using UserManagement.Models;
using UserManagement.Utility;

namespace UserManagement.Services
{
	public class UserService
	{
		private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public UserService(ApplicationDbContext context, IConfiguration configuration)
		{
			_context = context;
            _configuration = configuration;
		}

		public void RegisterUser(UserRegisterModel model)
		{
			if (_context.Users.Any(u => u.email == model.email))
			{
				throw new Exception("Email is already in use.");
			}

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.password);

			var newUser = new User
			{
				name = model.name,
				email = model.email,
				password = hashedPassword,
				role = "User"
			};

			_context.Users.Add(newUser);
			_context.SaveChanges();
        }

		public string AuthenticateUser(UserLoginModel model)
		{
			var user = _context.Users.SingleOrDefault(u => u.email == model.email);

			if (user == null || !BCrypt.Net.BCrypt.Verify(model.password, user.password))
			{
				return null;
			}

			// Generate Jwt Token
			var token = GenerateJwtToken(user.id.ToString(), user.role);

			return token;
		}

        public UserDataModel GetUserDataById(string userId)
        {
            // Fetch user data from the database by user ID
            var user = _context.Users.FirstOrDefault(u => u.id == Convert.ToInt32(userId));

            if (user != null)
            {
                // Create a model to return the required user data
                var userData = new UserDataModel
                {
                    name = user.name,
                    email = user.email
                };

                return userData;
            }
            return null; // Return null if user data not found
        }

        public string GenerateJwtToken(string userId, string userRole)
        {
            var jwtKey = _configuration["Jwt:Key"];
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)); // Use a secure key
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, userId), // User's unique identifier
            new Claim(ClaimTypes.Role, userRole) // User's role
            // Add more claims if needed
        };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1), // Token expiration time
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

