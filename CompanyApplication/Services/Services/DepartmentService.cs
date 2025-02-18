using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        public async Task CreateAsync(Department department)
        {
            try
            {
                if (department == null)
                {
                    throw new ArgumentNullException(nameof(department), "Departament məlumatları boş ola bilməz.");
                }
                    await _departmentRepository.CreateAsync(department);
   
            }
            catch (Exception ex)
            {              
                throw new Exception("Departament əlavə edilərkən xəta baş verdi.");
            }
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Department>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Department> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Department>> SearchAsync(string searchName)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(int id, Department department)
        {
            throw new NotImplementedException();
        }
         
    }
}