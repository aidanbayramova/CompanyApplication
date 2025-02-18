using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Service.Services.Interfaces
{
    public interface IDepartmentService
    {
        Task CreateAsync(Department department);
        Task UpdateAsync(int id,Department department);
        Task DeleteAsync(int id);
        Task<Department> GetByIdAsync(int id);
        Task<List<Department>> GetAllAsync();
        Task<List<Department>> SearchAsync(string searchName);
    }
}
