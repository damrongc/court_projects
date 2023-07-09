using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;
using CourtJustice.Infrastructure.Interfaces;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace CourtJustice.Infrastructure.Repositories
{
    public class EmployeeRepository : BaseRepository, IEmployeeRepository
    {
        public EmployeeRepository(IConfiguration config, ApplicationDbContext context) : base(config, context)
        {
        }

        public Task<int> CheckExistingAtUser(int id)
        {
            throw new NotImplementedException();
        }

        public async Task Create(Employee model)
        {
            await Context.Employees.AddAsync(model);
            await Context.SaveChangesAsync();
        }

        public async Task Delete(string id)
        {
            var model = await Context.Employees.FindAsync(id);
            Context.Employees.Remove(model);
            await Context.SaveChangesAsync();
        }

        public async Task<List<EmployeeViewModel>> GetAll()
        {
            try
            {
                using IDbConnection conn = Connection;
                conn.Open();
                var sql = @"select employee_code,
employee_name,
email,
phone_number,
target,
address,
address_detail,
is_active,
(select user_name from app_user where app_user.user_id=manager_code) as manager_name
from employee";
                var models = await conn.QueryAsync<EmployeeViewModel>(sql);
                return models.ToList();
            }
            catch (Exception)
            {

                throw;
            }
            //return await Context.Employees.ToListAsync();
        }

        public async Task<Employee> GetByKey(string id)
        {
            var model = await Context.Employees.FindAsync(id);
            return model;
        }

        public async Task<List<EmployeeViewModel>> GetEmployeeByCode(string employeeCode)
        {
            try
            {


                using IDbConnection conn = Connection;
                conn.Open();
                var sql = @"select employee_code,
employee_name,
email,
phone_number,
target,
address,
address_detail,
is_active,
(select user_name from app_user where app_user.user_id=manager_code) as manager_name
from employee
where employee_code=@employee_code";
                var models = await conn.QueryAsync<EmployeeViewModel>(sql, new { employee_code = employeeCode });
                return models.ToList();
            }
            catch
            {

                throw;
            }
        }

        public async Task<List<EmployeeViewModel>> GetEmployeeByManager(string managerCode)
        {
            try
            {


                using IDbConnection conn = Connection;
                conn.Open();
                var sql = @"select employee_code,
employee_name,
email,
phone_number,
target,
address,
address_detail,
is_active,
(select user_name from app_user where app_user.user_id=manager_code) as manager_name
from employee
where manager_code=@manager_code";
                var models = await conn.QueryAsync<EmployeeViewModel>(sql, new { manager_code = managerCode });
                return models.ToList();
            }
            catch
            {

                throw;
            }
        }

        public async Task Update(string id, Employee model)
        {
            var result = await Context.Employees.FindAsync(model.EmployeeCode);
            result.EmployeeName = model.EmployeeName;
            result.Address = model.Address;
            result.AddressDetail = model.AddressDetail;
            result.Email = model.Email;
            //result.HireDate = model.HireDate;
            result.Target = model.Target;
            result.ManagerCode = model.ManagerCode;
            result.IsActive = model.IsActive;
            await Context.SaveChangesAsync();
        }
    }
}

