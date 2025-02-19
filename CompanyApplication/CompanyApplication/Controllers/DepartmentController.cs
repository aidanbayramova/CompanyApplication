using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Domain.Entities;
using Service.Services;
using Service.Services.Interfaces;

namespace CompanyApplication.Controllers
{
    public class DepartmentController
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController()
        {
            _departmentService = new DepartmentService();
        }

        public async Task CreateAsync()
        {
            try
            {

                Console.WriteLine("Add Department Name");
                string departmentName = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(departmentName))
                {
                    Console.WriteLine("Department adı boş ola bilməz. Zəhmət olmasa, yenidən cəhd edin.");

                }

                if (Regex.IsMatch(departmentName, @"\d"))
                {
                    Console.WriteLine("Department adı rəqəm ehtiva edə bilməz. Zəhmət olmasa, yenidən cəhd edin.");
                    return;
                }

                if (departmentName == null)
                {
                    Console.WriteLine("Departament məlumatları boş ola bilməz.");
                }

                int departmentCapacity;
                Console.WriteLine("Add Department Capacity:");

                if (!int.TryParse(Console.ReadLine(), out departmentCapacity))
                {
                    Console.WriteLine("Zəhmət olmasa, kapasite üçün düzgün bir rəqəm daxil edin.");

                }
                if (departmentCapacity < 0)
                {
                    Console.WriteLine("Department kapasitesi mənfi ola bilməz. Zəhmət olmasa, yenidən cəhd edin.");
                }

                Department department = (new Department { Name = departmentName, Capacity = departmentCapacity });
                await _departmentService.CreateAsync(department);
                Console.WriteLine("Department Created Successfully");
            }
            catch (Exception ex)
            {

                Console.WriteLine("An error occurred while creating department.");
            }

        }

        public async Task DeleteAsync()
        {
            Console.WriteLine("Add Id For Deleting");
            int id = int.Parse(Console.ReadLine());
            try
            {
                await _departmentService.DeleteAsync(id);
                Console.WriteLine("Deleted Successfully");

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

        }
        public async Task SearchAsync()
        {
            try
            {
                await Console.Out.WriteLineAsync("Add Search Name:");
                string searchName = Console.ReadLine();
                var departments = await _departmentService.SearchAsync(searchName);
             
                foreach (var item in departments)
                {
                    Console.WriteLine($"{item.Name},{item.Capacity}");
                }
                if (departments == null || !departments.Any())
                {
                    Console.WriteLine($"Adı '{searchName}' olan heç bir departament tapılmadı.");

                }
            }
            catch (Exception ex)
            {
               
               Console.WriteLine($"Daxili server xətası: {ex.Message}");
            }
        }
        
        public async Task GetAllAsync()
        {
            try
            {
                var  departments = await _departmentService.GetAllAsync();

                foreach (var item in departments)
                {
                    Console.WriteLine($"{item.Name},{item.Capacity}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Internal server error: {ex.Message}"); 
            }

        }

        public async Task GetDepartmentIdAsync()
        {
            try
            {
                Console.WriteLine("Add Department Id:");
                int id = int.Parse(Console.ReadLine());
                var department = await _departmentService.GetDepartmentIdAsync(id);

                Console.WriteLine($"{department.Name},{department.Capacity}");

                if (department == null)
                {
                    Console.WriteLine($"ID-sı {id} olan departament tapılmadı.");
                }
             
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Daxili server xətası: {ex.Message}"); 
            }
        }
        public async Task UpdateAsync()
        {
            try
            {
                Console.WriteLine("Yeniləmək istədiyiniz departamentin ID-sini daxil edin:");
                if (!int.TryParse(Console.ReadLine(), out int departmentId))
                {
                    Console.WriteLine("Zəhmət olmasa, düzgün departament ID-si daxil edin.");
                    return;
                }
              
                var existingDepartment = await _departmentService.GetDepartmentIdAsync(departmentId);
                if (existingDepartment == null)
                {
                    Console.WriteLine("Departament tapılmadı.");
                    return;
                }
               
                Console.WriteLine($"Yeni Departament Adını daxil edin (Cari Ad: {existingDepartment.Name}):");
                string departmentName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(departmentName))
                {
                    departmentName = existingDepartment.Name;
                }

                Console.WriteLine($"Yeni Departament Kapasitesini daxil edin (Cari Kapasite: {existingDepartment.Capacity}):");
                if (!int.TryParse(Console.ReadLine(), out int departmentCapacity) || departmentCapacity < 0)
                {
                    departmentCapacity = existingDepartment.Capacity; 
                    Console.WriteLine("Kapasite boş olduğu üçün əvvəlki dəyər saxlanıldı.");
                }

                var department = new Department { Name = departmentName, Capacity = departmentCapacity };
                await _departmentService.UpdateAsync(departmentId, department);
                Console.WriteLine("Departament uğurla yeniləndi.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Departament yenilənərkən xəta baş verdi: {ex.Message}");
            }
        }
    }   
}

