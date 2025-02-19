using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Service.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task CreateAsync(Employee employee);
        Task UpdateAsync(int id, Employee employee);
        Task<Employee> GetByIdAsync(int id);
        Task DeleteAsync(int id);
        Task<Employee> GetByAgeAsync(int age);
        Task<Employee> GetDepartmentById(int departmentId);
        Task<List<Employee>> GetAllAsync();
        Task<List<Employee>> GetAllDepartmentByNameAsync(string departmentName);
        Task<List<Employee>> SearchNameOrSurnameAsync(string searchText);
        Task<int> GetAllCountAsync();

    }
}
