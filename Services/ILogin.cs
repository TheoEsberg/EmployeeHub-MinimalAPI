namespace EmployeeHub_MinimalAPI.Services
{
	public interface ILogin<T>
	{
		Task<T> Login(string username, string password);
	}
}
