﻿using EmployeeHub_MinimalAPI.Services.Password;

namespace EmployeeHub_MinimalAPI.Services
{
	public interface ILogin<T>
	{
		Task<T> Login(string email, string password, PasswordHashingService passwordHashingService);
	}
}