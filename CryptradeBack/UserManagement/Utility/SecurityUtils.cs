using System;
namespace UserManagement.Utility
{
	public static class SecurityUtils
	{
		public static string GenerateSecurityKey(int keyLength)
		{
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
			var random = new Random();
			var chars = new char[keyLength];

			for (int i = 0; i < keyLength; i++)
			{
				chars[i] = validChars[random.Next(validChars.Length)];
			}

			return new string(chars);
        }
    }
}

