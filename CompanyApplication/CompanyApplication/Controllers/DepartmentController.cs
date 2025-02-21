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
                await Console.Out.WriteLineAsync("Add Search Name:");
            SearchName: string searchName = Console.ReadLine();

                var departments = await _departmentService.GetAllAsync();

                if (string.IsNullOrWhiteSpace(searchName))
                {
                    foreach (var item in departments)
                    {
                        Console.WriteLine($"Id:{item.Id},Name:{item.Name},Capacity:{item.Capacity},CreateDate:{item.CreateDate}");
                    }

                }            
                if (departments == null || !departments.Any())
                {
                    Console.WriteLine($"Adı '{searchName}' olan heç bir departament tapılmadı.");
                    goto SearchName;
                }

                var data = await _departmentService.SearchAsync(searchName);
                foreach (var item in data)
                {
                    Console.WriteLine($"Id:{item.Id},Name:{item.Name},Capacity:{item.Capacity},CreateDate:{item.CreateDate}");
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
                    Console.WriteLine($"Id{department.Id},Name:{department.Name}, Capacity:{department.Capacity},CreateDate:{department.CreateDate}");
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
                var Departmets = await _departmentService.GetAllAsync();
               Console.WriteLine("Yeniləmək istədiyiniz departamentin ID-sini daxil edin:");
                string idStr= Console.ReadLine();
                int id;
                if (!int.TryParse(idStr, out id))
                {
                    Console.WriteLine("Zəhmət olmasa, düzgün departament ID-si daxil edin.");

                }

                if(Departmets.All(x => x.Id == id))
                {
                    Console.WriteLine("NotFound");
                    
                }

               
                Console.WriteLine($"Yeni Departament Adını daxil edin ");
            N: string departmentName = Console.ReadLine();
                var existingDepartment = await _departmentService.GetByIdAsync(id);
                if (string.IsNullOrWhiteSpace(departmentName))
                {
                    departmentName = existingDepartment.Name;
                }

                Console.WriteLine($"Yeni Departament Kapasitesini daxil edin ");
                string departmentkapa= Console.ReadLine();
                int newcapacity=0;
                if (!string.IsNullOrWhiteSpace(departmentkapa))
                {
                    if (!int.TryParse(departmentkapa, out newcapacity) )
                    {
                        Console.WriteLine("wrong format");
                    }
                    if (newcapacity < 1)
                    {
                        Console.WriteLine("wrong  format");
                    }

                }
                              
                await _departmentService.UpdateAsync(id, new Department { Name = departmentName,Capacity = newcapacity });
                
                Console.WriteLine("Departament uğurla yeniləndi.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Departament yenilənərkən xəta baş verdi: {ex.Message}");
            }
        }        
    }   
}

