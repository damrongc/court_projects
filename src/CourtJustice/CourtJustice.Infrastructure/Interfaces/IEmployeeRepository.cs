using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;

namespace CourtJustice.Infrastructure.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<List<EmployeeViewModel>> GetAll();
        Task Create(Employee model);
        Task Update(string id, Employee model);
        Task Delete(string id);
        Task<Employee> GetByKey(string id);
        Task<int> CheckExistingAtUser(int id);
        Task<List<EmployeeViewModel>> GetEmployeeByManager(string managerCode);
        Task<List<EmployeeViewModel>> GetEmployeeByCode(string employeeCode);
    }
}

