using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
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

            if (department == null)
            {
                throw new ArgumentNullException(nameof(department));
            }

            //if (string.IsNullOrWhiteSpace(department.Name))
            //{
            //    throw new ArgumentException("Departament adı boş ola bilməz.");
            //}

            //if (department.Capacity < 0)
            //{
            //    throw new ArgumentException("Departament kapasitesi mənfi ola bilməz.");
            //}

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
            if (string.IsNullOrWhiteSpace(searchName))
            {
                return await GetAllAsync();
            }
            
            return await _departmentRepository.SearchAsync(searchName);
        }

        public async Task UpdateAsync(int id, Department department)
        {
            var existingDepartment = await _departmentRepository.GetByIdAsync(id);
           

            if (existingDepartment == null)
            {
                throw new ArgumentException("Departament tapılmadı.");
            }

            existingDepartment.Name = department.Name;
            existingDepartment.Capacity = department.Capacity;   
        }
    }
}