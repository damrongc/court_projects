using CourtJustice.Domain.Models;
using CourtJustice.Domain.ViewModels;
using CourtJustice.Infrastructure.Helpers;
using CourtJustice.Infrastructure.Interfaces;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
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

        public async Task<List<Employee>> GetAll()
        {
            return await Context.Employees.ToListAsync();
        }

        public async Task<Employee> GetByKey(string id)
        {
            var model = await Context.Employees.FindAsync(id);
            return model;
        }

        public async Task Update(string id, Employee model)
        {
            var result = await Context.Employees.FindAsync(model.EmployeeCode);
            //result.EmployeeName = model.EmployeeName;
            //ทำไมถึง เป็น set init
            await Context.SaveChangesAsync();
        }
    }
}

