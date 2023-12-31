﻿using Microsoft.EntityFrameworkCore;
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
				password = hashedPassword,
				role = "User"
			};

			_context.Users.Add(newUser);
			_context.SaveChanges();
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            var listOfUsers = await _context.Users.ToListAsync();
            return listOfUsers;
        }

        public UserDataModel GetUserDataById(string userId)
        {
            // Fetch user data from the database by user ID
            var user = _context.Users.FirstOrDefault(u => u.id == Convert.ToInt32(userId));

            if (user != null)
            {
                var userData = new UserDataModel
                {
                    name = user.name,
                    email = user.email,
                    id = user.id
                };

                return userData;
            }
            return null; // Return null if user data not found
        }

        public void UpdateUserData(string userId, string newName, string newEmail)
        {
            var user = _context.Users.Find(Convert.ToInt32(userId));

            if (user != null)
            {
                if (_context.Users.Any(u => u.email == newEmail && u.id != Convert.ToInt32(userId)))
                {
                    throw new Exception("Email is already in use.");
                }
                else
                {
                    user.name = newName;
                    user.email = newEmail;
                    _context.SaveChanges();
                }
            }
        }

        public void DeleteUser(string userId)
        {
            var user = _context.Users.Find(Convert.ToInt32(userId));

            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }
    }
}

