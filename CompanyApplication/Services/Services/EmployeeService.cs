using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Service.Services.Interfaces;

namespace Service.Services
{
    internal class EmployeeService : IEmployeeService
    {
        public Task CreateAsync(Employee employee)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Employee>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> GetAllCountAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<Employee>> GetAllDepartmentByNameAsync(string departmentName)
        {
            throw new NotImplementedException();
        }

        public Task<Employee> GetByAgeAsync(int age)
        {
            throw new NotImplementedException();
        }

        public Task<Employee> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Employee> GetDepartmentById(int departmentId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Employee>> SearchNameOrSurnameAsync(string searchText)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(int id, Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}
