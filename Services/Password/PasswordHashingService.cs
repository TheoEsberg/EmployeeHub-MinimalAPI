using BCrypt;
using EmployeeHub_MinimalAPI.Migrations;

namespace EmployeeHub_MinimalAPI.Services.Password
{
	public class PasswordHashingService
	{
		public string HashPassword(string password, string salt)
		{
			// Hash the password with the generated salt
			return BCrypt.Net.BCrypt.HashPassword(password, salt);
		}

		public bool VerifyPassword(string password, string hashedPassword) 
		{ 
			// Verify the password against the stored hash
			
			return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
		}

		public string GenerateSalt()
		{
			return BCrypt.Net.BCrypt.GenerateSalt();
		}
	}
}
