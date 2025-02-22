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
     Name: string departmentName = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(departmentName))
                {
                    Console.WriteLine("Department adı boş ola bilməz. Zəhmət olmasa, yenidən cəhd edin.");
                    goto Name;
                }

                if (Regex.IsMatch(departmentName, @"[\d\W_]"))
                {
                    Console.WriteLine("Department adı rəqəm və ya xüsusi işarələr (məsələn, @, #, &, ...) ehtiva edə bilməz. Zəhmət olmasa, yenidən cəhd edin.");
                    goto Name;
                }
                var deparments = await _departmentService.GetAllAsync();
                var existingDepartment = deparments.FirstOrDefault(x=>x.Name.Trim().ToLower()== departmentName.ToLower().Trim());
                if (existingDepartment != null)
                {
                    Console.WriteLine($"'{departmentName}' adlı departament artıq mövcuddur. Zəhmət olmasa, fərqli bir ad daxil edin.");
                    goto Name;
                }



                Console.WriteLine("Add Department Capacity:");
     Capacity:  string input = Console.ReadLine();

                if (!int.TryParse(input, out int departmentCapacity))
                {
                    Console.WriteLine("Zəhmət olmasa, kapasite üçün düzgün bir rəqəm daxil edin.");
                    goto Capacity;
                }
                if (Regex.IsMatch(input, @"[^\d]"))
                {
                    Console.WriteLine("Capacity yalnız rəqəmlərdən ibarət olmalıdır. Zəhmət olmasa, yenidən cəhd edin.");
                    goto Capacity;
                }
                if (departmentCapacity <= 0)
                {
                    Console.WriteLine("Department kapasitesi müsbət və 0-dan böyük olmalıdır. Zəhmət olmasa, yenidən cəhd edin.");
                    goto Capacity;
                }


                Department department = new Department()
                {
                    Name = departmentName,
                    Capacity = departmentCapacity

                };
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
            try
            {
                Console.WriteLine("Add Id For Deleting");
            Id: string input = Console.ReadLine();
                int id;

                if (!int.TryParse(input, out id))
                {
                    Console.WriteLine("invalid");
                    goto Id;
                }
                var department = await _departmentService.GetByIdAsync(id);
                if (department != null)
                {
                    await _departmentService.DeleteAsync(id);
                    Console.WriteLine("Deleted Successfully");
                }
                else
                {
                    Console.WriteLine("department not found");
                    goto Id;
                }
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
            Start:
                await Console.Out.WriteLineAsync("Add Search Name:");
                string searchName = Console.ReadLine()?.Trim(); // Trim() boşluqları silir

                var departments = await _departmentService.GetAllAsync();

                // Əgər sistemdə heç bir departament yoxdursa
                if (departments == null || !departments.Any())
                {
                    Console.WriteLine("Sistemdə heç bir departament yoxdur.");
                    return;
                }

                // Əgər istifadəçi boş dəyər daxil edərsə (Enter və ya Space)
                if (string.IsNullOrWhiteSpace(searchName))
                {
                    foreach (var item in departments)
                    {
                        Console.WriteLine($"Id:{item.Id}, Name:{item.Name}, Capacity:{item.Capacity}, CreateDate:{item.CreateDate}");
                    }
                    return;
                }

                // Departament axtarışı
                var data = await _departmentService.SearchAsync(searchName);

                // Əgər heç bir departament tapılmayıbsa
                if (data == null || !data.Any())
                {
                    Console.WriteLine($"Departament tapılmadı. Yenidən cəhd et:");
                    goto Start; // Yenidən daxil etmə istəyir
                }

                // Tapılan departamentləri göstər
                foreach (var item in data)
                {
                    Console.WriteLine($"Id:{item.Id}, Name:{item.Name}, Capacity:{item.Capacity}, CreateDate:{item.CreateDate}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Daxili server xətası: {ex.Message}");
            }

        }
        

        public async Task GetAllAsync()
        {
                      
                var  departments = await _departmentService.GetAllAsync();

                foreach (var item in departments)
                {
                    Console.WriteLine($"Id:{item.Id},Name:{item.Name},Capacity:{item.Capacity},CreateDate:{item.CreateDate}");
                }
            
        }

        public async Task GetByIdAsync()
        {
            try
            {
                Console.WriteLine("Add Department Id:");
        I:  string input = Console.ReadLine();

                if (Regex.IsMatch(input, @"[^\d]") || input.StartsWith("-"))
                {
                    Console.WriteLine("Department Id yalnız müsbət rəqəm olmalıdır və mənfi işarə və xüsusi simvollar daxil edilə bilməz.");
                    goto I;
                }
                if (!int.TryParse(input, out int id))
                {
                    Console.WriteLine("Zəhmət olmasa, düzgün bir Department Id daxil edin.");
                    goto I;
                }

                var department = await _departmentService.GetByIdAsync(id);

                if (department == null)
                {
                    Console.WriteLine($"ID-sı {id} olan departament tapılmadı.");
                    goto I;
                }
                else
                {
                    Console.WriteLine($"Id:{department.Id},Name:{department.Name}, Capacity:{department.Capacity},CreateDate:{department.CreateDate}");
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
            Start:
                var departments = await _departmentService.GetAllAsync();
                Console.WriteLine("Yeniləmək istədiyiniz departamentin ID-sini daxil edin:");
                string idStr = Console.ReadLine();
                int id;

                if (!int.TryParse(idStr, out id))
                {
                    Console.WriteLine("Zəhmət olmasa, düzgün departament ID-si daxil edin.");
                    goto Start; // Düzgün ID daxil edilmədikdə yenidən ID soruşuruq
                }

                var existingDepartment = await _departmentService.GetByIdAsync(id);
                if (existingDepartment == null)
                {
                    Console.WriteLine("Departament tapılmadı.");
                    goto Start; // Departament tapılmadıqda yenidən ID soruşuruq
                }


                Console.WriteLine($"Yeni Departament Adını daxil edin (mövcud: {existingDepartment.Name}): ");
                string departmentName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(departmentName))
                {
                    departmentName = existingDepartment.Name; // Əgər boş buraxılıbsa, əvvəlki adı saxlayırıq
                }

                var departmentExists = departments.Any(d => d.Name.Equals(departmentName, StringComparison.OrdinalIgnoreCase) && d.Id != id);
                if (departmentExists)
                {
                    Console.WriteLine($"'{departmentName}' adı artıq mövcuddur, zəhmət olmasa başqa ad daxil edin.");
                    goto Start; // Yeni ad mövcud olduqda, başqa ad soruşuruq
                }

                Console.WriteLine($"Yeni Departament Kapasitesini daxil edin (mövcud: {existingDepartment.Capacity}): ");
                string departmentCapa = Console.ReadLine();
                int newCapacity = (int)existingDepartment.Capacity; // Varsayılan olaraq əvvəlki kapasiteyi alırıq

                if (!string.IsNullOrWhiteSpace(departmentCapa))
                {
                    if (!int.TryParse(departmentCapa, out newCapacity) || newCapacity < 1)
                    {
                        Console.WriteLine("Kapasite səhv formatda daxil edilib.");
                        goto Start; // Yanlış format daxil edildikdə yenidən soruşuruq
                    }
                }

                // Yenilənmiş dəyərləri departamentə tətbiq edirik
                await _departmentService.UpdateAsync(id, new Department { Name = departmentName, Capacity = newCapacity });

                Console.WriteLine("Departament uğurla yeniləndi.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Departament yenilənərkən xəta baş verdi: {ex.Message}");
            }
        }        

        
    }   
}

