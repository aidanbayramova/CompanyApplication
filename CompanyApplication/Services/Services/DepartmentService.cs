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
                throw new ArgumentNullException(nameof(department), "Departament məlumatları boş ola bilməz.");
            }

            if (string.IsNullOrWhiteSpace(department.Name))
            {
                throw new ArgumentException("Departament adı boş ola bilməz.");
            }

            if (department.Capacity < 0)
            {
                throw new ArgumentException("Departament kapasitesi mənfi ola bilməz.");
            }

            await _departmentRepository.CreateAsync(department);
        }

        public async Task DeleteDepartmentAsync(int id)
        {
            await _departmentRepository.DeleteDepartmentAsync(id);
        }

        public  async Task<List<Department>> GetAllAsync()
        {
            return await _departmentRepository.Departments.ToListAsync();
        }

     

        public async Task<Department> GetDepartmentIdAsync(int id)
        {
            return await _departmentRepository.GetDepartmentIdAsync();
        }

        public async Task<List<Department>> SearchAsync(string searchName)
        {
            return await _departmentRepository.Departments.Where(m => m.Name.Contains(searchName)).ToListAsync();
        }

        public async Task UpdateAsync(int id, Department department)
        {
            var existingDepartment = await _departmentRepository.Departments.GetByIdAsync(departmentId);

            if (existingDepartment == null)
            {
                throw new ArgumentException("Departament tapılmadı.");
            }

           
            existingDepartment.Name = department.Name;
            existingDepartment.Capacity = department.Capacity;

            _departmentRepository.Departments.Update(existingDepartment);
            await _departmentRepository.UpdateAsync(department);
        }

        

    }
}