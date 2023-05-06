using CourtJustice.Domain.Models;

namespace CourtJustice.Infrastructure.Interfaces
{
	public interface IEmployeeRepository
	{

        Task<List<Employee>> GetAll();
        Task Create(Employee model);
        Task Update(string id, Employee model);
        Task Delete(string id);
        Task<Employee> GetByKey(string id);

        Task<int> CheckExistingAtUser(int id);
    }
}

