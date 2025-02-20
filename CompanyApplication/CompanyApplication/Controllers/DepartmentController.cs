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

                if (Regex.IsMatch(departmentName, @"[\d\W_]"))
                {
                    Console.WriteLine("Department adı rəqəm və ya xüsusi işarələr (məsələn, @, #, &, ...) ehtiva edə bilməz. Zəhmət olmasa, yenidən cəhd edin.");
                    return;
                }

                if (departmentName == null)
                {
                    Console.WriteLine("Departament məlumatları boş ola bilməz.");
                }
                var existingDepartment = await _departmentService.SearchAsync(departmentName);
                if (existingDepartment.Any())
                {
                    Console.WriteLine($"'{departmentName}' adlı departament artıq mövcuddur. Zəhmət olmasa, fərqli bir ad daxil edin.");
                    return;
                }

                Console.WriteLine("Add Department Capacity:");
                string input = Console.ReadLine();

                if (!int.TryParse(input, out int departmentCapacity))
                {
                    Console.WriteLine("Zəhmət olmasa, kapasite üçün düzgün bir rəqəm daxil edin.");
                    return;
                }
                if (Regex.IsMatch(input, @"[^\d]"))
                {
                    Console.WriteLine("Capacity yalnız rəqəmlərdən ibarət olmalıdır. Zəhmət olmasa, yenidən cəhd edin.");
                    return;
                }
                if (departmentCapacity <= 0)
                {
                    Console.WriteLine("Department kapasitesi müsbət və 0-dan böyük olmalıdır. Zəhmət olmasa, yenidən cəhd edin.");
                    return;
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
                if (Regex.IsMatch(id.ToString(), @"\D|^-"))
                {
                    Console.WriteLine("Id yalnız rəqəmlərdən ibarət olmalıdır ve menfi olmaz. Zəhmət olmasa, yenidən cəhd edin.");
                    return;
                }

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

                if (Regex.IsMatch(searchName, @"[\d\W_]|^-"))
                {
                    Console.WriteLine("Ad yalnız hərflərdən ibarət olmalıdır və mənfi ədəd, rəqəm və xüsusi işarələrdən istifadə edilməməlidir.");
                    return;
                }

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
                    Console.WriteLine($"{item.Id},{item.Name},{item.Capacity},{item.CreateDate}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Internal server error: {ex.Message}"); 
            }

        }

        public async Task GetByIdAsync()
        {
            try
            {
                Console.WriteLine("Add Department Id:");
                string input = Console.ReadLine();

                if (Regex.IsMatch(input, @"[^\d]") || input.StartsWith("-"))
                {
                    Console.WriteLine("Department Id yalnız müsbət rəqəm olmalıdır və mənfi işarə və xüsusi simvollar daxil edilə bilməz.");
                    return;
                }
                if (!int.TryParse(input, out int id))
                {
                    Console.WriteLine("Zəhmət olmasa, düzgün bir Department Id daxil edin.");
                    return;
                }

                var department = await _departmentService.GetByIdAsync(id);

                if (department == null)
                {
                    Console.WriteLine($"ID-sı {id} olan departament tapılmadı.");
                }
                else
                {
                    Console.WriteLine($"{department.Name}, {department.Capacity},{department.CreateDate}");
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
              
                var existingDepartment = await _departmentService.GetByIdAsync(departmentId);
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

