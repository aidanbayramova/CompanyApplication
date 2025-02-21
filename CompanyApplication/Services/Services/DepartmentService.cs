using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Helpers.Exceptions;
using Repository.Repositories;
using Repository.Repositories.Interfaces;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        public DepartmentService()
        {
            _departmentRepository = new DepartmentRepository();
        }

        public async Task CreateAsync(Department department)
        {
            await _departmentRepository.CreateAsync(department);
        }

        public async Task DeleteAsync(int id)
        {
            var department = await _departmentRepository.GetByIdAsync(id);
            if (department is null)
            {
                //throw new NotFoundException("Education not found");
            }
            
            _departmentRepository.DeleteAsync(department);
         
        }

        public  async Task<IEnumerable<Department>> GetAllAsync()
        {
            return await _departmentRepository.GetAllAsync();
        }

     

        public async Task<Department> GetByIdAsync(int id)
        {
            return await _departmentRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Department>> SearchAsync(string searchName)
        {
            var departament = await _departmentRepository.SearchAsync(searchName);

            if(departament.Count == 0)
            {
                throw new NotFoundException("Departament tapilmadi");
            }

            return departament;
            
            
        }

        public async Task UpdateAsync(int id, Department department)
        {
            var existingDepartment = await _departmentRepository.GetByIdAsync(id);
           if(!string.IsNullOrWhiteSpace(department.Name))
            {
                existingDepartment.Name = department.Name;
            }

           if(department.Capacity > 0)
            {
                existingDepartment.Capacity = department.Capacity;
            }
            await _departmentRepository.UpdateAsync(existingDepartment);

           
        }
    }
}