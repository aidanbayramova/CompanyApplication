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
            if (string.IsNullOrWhiteSpace(searchName)) // Əgər boş və ya sadəcə boşluq daxil edilibsə
            {
                return await _departmentRepository.GetAllAsync(); // Bütün departamentləri qaytar
            }

            var departments = await _departmentRepository.SearchAsync(searchName.Trim()); // Axtarış üçün trim et

            return departments; // Boş nəticə qayıtsa belə, Exception atmırıq, Controller özü yoxlayacaq   


        }

        public async Task UpdateAsync(int id, Department department)
        {
            // Departamenti ID-sinə görə əldə edirik
            var existingDepartment = await _departmentRepository.GetByIdAsync(id);

            // Əgər departament tapılmasa, NotFoundException atırıq
            if (existingDepartment == null)
            {
                throw new NotFoundException("Departament tapılmadı.");
            }

            // Adı yeniləyirik, amma yalnız boş deyilsə
            if (!string.IsNullOrWhiteSpace(department.Name))
            {
                existingDepartment.Name = department.Name;
            }

            // Kapasiteyi yeniləyirik, amma yalnız müsbət ədədlər qəbul edilir
            if (department.Capacity > 0)
            {
                existingDepartment.Capacity = department.Capacity;
            }

            // Yenilənmiş departamenti saxlayırıq
            await _departmentRepository.UpdateAsync(existingDepartment);


        }
    }
}