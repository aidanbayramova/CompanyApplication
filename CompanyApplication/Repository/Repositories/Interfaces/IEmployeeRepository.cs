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
        Task<Employee> GetByDepartmentId(int departmentId);
        Task<IEnumerable<Employee>> GetAllByDepartmentNameAsync(string departmentName);
        Task<IEnumerable<Employee>> GetAllCountAsync();

    }
}
