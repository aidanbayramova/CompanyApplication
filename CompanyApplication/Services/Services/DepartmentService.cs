using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Helpers;
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
                throw new NotFoundException(ResponseMessages.NotFound);
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
                return await _departmentRepository.GetAllAsync(); 
            }

            var departments = await _departmentRepository.SearchAsync(searchName.Trim());

            return departments; 
        }

        public async Task UpdateAsync(int id, Department department)
        {
            var existingDepartment = await _departmentRepository.GetByIdAsync(id);

            if (existingDepartment == null)
            {
                throw new NotFoundException(ResponseMessages.DepartmentNotFound);
            }

            if (!string.IsNullOrWhiteSpace(department.Name))
            {
                existingDepartment.Name = department.Name;
            }
            if (department.Capacity > 0)
            {
                existingDepartment.Capacity = department.Capacity;
            }
            await _departmentRepository.UpdateAsync(existingDepartment);


        }
    }
}