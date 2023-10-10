namespace EmployeeHub_MinimalAPI.Services
{
	public interface IRepository<T>
	{
		Task<IEnumerable<T>> GetAllAsync();
		Task<T> GetAsync(int id);
		Task<T> CreateAsync(T entity);
		Task<T> UpdateAsync(T entity);
		Task<T> DeleteAsync(int id);
	}
}
