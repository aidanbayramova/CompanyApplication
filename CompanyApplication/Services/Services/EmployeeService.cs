using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Helpers.Exceptions;
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

        public async Task<int> GetAllCountAsync()
        {
            var employeeCount = await _employeeRepository.GetAllCountAsync();
            return employeeCount;
        }

        public async Task<List<Employee>> GetAllDepartmentByNameAsync(string departmentName)
        {
           return await _employeeRepository.GetAllDepartmentByNameAsync(departmentName);
        }

        public async Task<List<Employee>> GetByAgeAsync(int age)
        {
           var emp = await _employeeRepository.GetByAgeAsync(age);
          if(emp is null)
          {
                throw new Exception($"Yaşı {age} olan employee tapılmadı.");
          }
           return emp;
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            return await _employeeRepository.GetByIdAsync(id);
        }

        public async Task<List<Employee>> GetDepartmentById(int departmentId)
        {
           return await _employeeRepository.GetDepartmentByIdAsync(departmentId);
        }

        public async Task<IEnumerable<Employee>> SearchNameOrSurnameAsync(string searchText)
        {
            var employees = await _employeeRepository.SearchNameOrSurnameAsync(searchText);

            if (employees.Count () > 0)
            {
                throw new NotFoundException("Departament tapilmadi");
            }
            return employees;
                
            

            
        }

        public async Task UpdateAsync(int id, Employee employee)
        {
            await _employeeRepository.GetByIdAsync(id);
        }
    }
}
