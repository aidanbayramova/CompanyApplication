using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService()
        {
            _employeeRepository = new EmployeeRepository();
        }
        public async Task CreateAsync(Employee employee)
        {
           await _employeeRepository.CreateAsync(employee);
        }

        public async Task DeleteAsync(int id)
        {
            var department = await _employeeRepository.GetByIdAsync(id);
            if (department is null)
            {
                //throw new NotFoundException("Education not found");
            }

            _employeeRepository.DeleteAsync(department);
        }

        public async Task<IEnumerable<Employee>> GetAllAsync()
        {
            return await _employeeRepository.GetAllAsync();
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

        public async Task UpdateAsync(int id, Employee employee)
        {
            await _employeeRepository.GetByIdAsync(id);
        }
    }
}
