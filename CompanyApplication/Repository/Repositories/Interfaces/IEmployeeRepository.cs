using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Repository.Repositories.Interfaces
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        Task <IEnumerable<Employee>> GetDepartmentById(int departmentId);
        Task<List<Employee>> GetAllDepartmentByNameAsync(string departmentName);
        Task<int> GetAllCountAsync();
        Task<List<Employee>> SearchNameOrSurnameAsync(string searchText);
        Task<Employee> GetByAgeAsync(int age);
    }
}
