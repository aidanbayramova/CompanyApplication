using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;
using Domain.Entities;

namespace Repository.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task CreateAsync(T entity);
        Task DeleteAsync(T entity);
        Task UpdateAsync(T entity);
    }
}
