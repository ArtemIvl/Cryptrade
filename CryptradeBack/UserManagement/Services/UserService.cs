using System;
using UserManagement.Data;
using UserManagement.Entity;
using UserManagement.Models;

namespace UserManagement.Services
{
	public class UserService
	{
		private readonly ApplicationDbContext _context;

		public UserService(ApplicationDbContext context)
		{
			_context = context;
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
				password = model.password,
				role = "User"
			};

			_context.Users.Add(newUser);
			_context.SaveChanges();
        }

		public User? AuthenticateUser(UserLoginModel model)
		{
			var user = _context.Users.SingleOrDefault(u => u.email == model.email);

			if (user == null || !BCrypt.Net.BCrypt.Verify(model.password, user.password))
			{
				return null;
			}

			return user;
		}
    }
}

