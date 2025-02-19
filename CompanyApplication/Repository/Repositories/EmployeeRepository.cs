using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public async Task<List<Employee>> GetAllDepartmentByNameAsync(string departmentName)
        {
            return await _context.Employees.Include(e => e.Department).Where(e => e.Department.Name.ToLower() == departmentName.ToLower()).ToListAsync();
        }

        public async Task<int> GetAllCountAsync()
        {
            return await _context.Employees.CountAsync();   
        }

        public async Task<Employee> GetByAgeAsync(int age)
        {
            return await _context.Employees.FirstOrDefaultAsync(e => e.Age == age);
        }

        public async Task<IEnumerable<Employee>> GetDepartmentById(int departmentId)
        {
            return await _context.Employees
                .Where(e => e.DepartmentId == departmentId)
                .ToListAsync();
        }

        public async Task<List<Employee>> SearchNameOrSurnameAsync(string searchText)
        {       
            return await _context.Employees.Where(e => e.Name.Contains(searchText) || e.Surname.Contains(searchText)).ToListAsync();
        }
    }

       
}










