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
        private readonly DbSet<Employee> _employeeSet;

        public EmployeeRepository()
        {
            _context = new AppDbContext();
            _employeeSet = _context.Set<Employee>();
        }

        public async Task<List<Employee>> GetAllDepartmentByNameAsync(string departmentName)
        {
            return await _context.Employees.Include(e => e.Department).Where(e => e.Department.Name.ToLower() == departmentName.ToLower()).ToListAsync();
        }

        public async Task<int> GetAllCountAsync()
        {
            return await _context.Employees.CountAsync();   
        }

        public async Task<List<Employee>> GetByAgeAsync(int age)
        {
            return await _employeeSet.Where(e => e.Age == age).ToListAsync();     
        }

        public async Task<List<Employee>> GetDepartmentByIdAsync(int departmentId)
        {
            return await _context.Employees.Where(e => e.DepartmentId == departmentId).ToListAsync();
        }

        public async Task<IEnumerable<Employee>> SearchNameOrSurnameAsync(string searchText)
        {       
            return await _context.Employees.Where(e => e.Name.Contains(searchText) || e.Surname.Contains(searchText)).ToListAsync();
        }
    }

       
}










