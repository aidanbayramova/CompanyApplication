using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Repository.Repositories.Interfaces
{
    public  interface IDepartmentRepository : IBaseRepository<Department>
    {
        Task<List<Department>> SearchAsync(string searchName);

        Task<List<Department>> GetAllAsync();

        Task<Department> GetByIdAsync(int id);

        Task AddAsync(Department department);

        Task UpdateAsync(Department department);

        Task DeleteAsync(int id);
    }
}
