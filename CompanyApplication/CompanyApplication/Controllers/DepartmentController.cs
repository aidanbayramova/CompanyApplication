using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Domain.Entities;
using Repository.Helpers.Contains;
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

                 Console.WriteLine("Add Department Name:");
          Name: string departmentName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(departmentName))
                {
                    Console.WriteLine(ValidationMessages.InputError);
                    goto Name;
                }

                if (Regex.IsMatch(departmentName, @"[\d\W_]"))
                {
                    Console.WriteLine(ValidationMessages.InvalidDepartmentName);
                    goto Name;
                }
                var deparments = await _departmentService.GetAllAsync();
                var existingDepartment = deparments.FirstOrDefault(x=>x.Name.Trim().ToLower()== departmentName.ToLower().Trim());
                if (existingDepartment != null)
                {
                    Console.WriteLine(ValidationMessages.NameConflictError);
                    goto Name;
                }

                Console.WriteLine("Add Department Capacity:");
     Capacity:  string input = Console.ReadLine();

                if (!int.TryParse(input, out int departmentCapacity))
                {
                    Console.WriteLine(ValidationMessages.CapacityValidationError);
                    goto Capacity;
                }
                if (Regex.IsMatch(input, @"[^\d]"))
                {
                    Console.WriteLine(ValidationMessages.NumericInputRequired);
                    goto Capacity;
                }
                if (departmentCapacity <= 0)
                {
                    Console.WriteLine(ValidationMessages.PositiveNumberRequired);
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

                Console.WriteLine(ValidationMessages.FailedtoCreateDepartment);
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
                    Console.WriteLine(ValidationMessages.InvalidIDFormat);
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
                    Console.WriteLine(ValidationMessages.NotFound);
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
          N:   string searchName = Console.ReadLine()?.Trim(); 

                var departments = await _departmentService.GetAllAsync();

               
                if (departments == null || !departments.Any())
                {
                    Console.WriteLine(ValidationMessages.NoDepartmentsFound);
                    return;
                }
               
                if (string.IsNullOrWhiteSpace(searchName))
                {
                    foreach (var item in departments)
                    {
                        Console.WriteLine($"Id:{item.Id}, Name:{item.Name}, Capacity:{item.Capacity}, CreateDate:{item.CreateDate}");
                    }
                    return;
                }
              
                var data = await _departmentService.SearchAsync(searchName);               
                if (data == null || !data.Any())
                {
                    Console.WriteLine(ValidationMessages.DepartmentNotFoundError);
                    goto N; 
                }              
                foreach (var item in data)
                {
                    Console.WriteLine($"Id:{item.Id}, Name:{item.Name}, Capacity:{item.Capacity}, CreateDate:{item.CreateDate}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
                    Console.WriteLine(ValidationMessages.InvalidDepartmentId);
                    goto I;
                }
                if (!int.TryParse(input, out int id))
                {
                    Console.WriteLine(ValidationMessages.InvalidIdFormat);
                    goto I;
                }

                var department = await _departmentService.GetByIdAsync(id);

                if (department == null)
                {
                    Console.WriteLine(ValidationMessages.InvalidDepartmentID);
                    goto I;
                }
                else
                {
                    Console.WriteLine($"Id:{department.Id},Name:{department.Name}, Capacity:{department.Capacity},CreateDate:{department.CreateDate}");
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
        public async Task UpdateAsync()
        {
            try
            {
           
                var departments = await _departmentService.GetAllAsync();
                Console.WriteLine("Please enter the ID of the department you want to update:");
             I:   string idStr = Console.ReadLine();
                int id;

                if (!int.TryParse(idStr, out id))
                {
                    Console.WriteLine(ValidationMessages.InvalidIDFormatt);
                    goto I; 
                }

                var existingDepartment = await _departmentService.GetByIdAsync(id);
                if (existingDepartment == null)
                {
                    Console.WriteLine(ValidationMessages.NotFound);
                    goto I; 
                }


                Console.WriteLine($"Please enter the new department name.");
            N:    string departmentName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(departmentName))
                {
                    departmentName = existingDepartment.Name; 
                }

                var departmentExists = departments.Any(d => d.Name.Equals(departmentName, StringComparison.OrdinalIgnoreCase) && d.Id != id);
                if (departmentExists)
                {
                    Console.WriteLine(ValidationMessages.NameConflictErrorr);
                    goto N;
                }

                Console.WriteLine($"Please enter the new department capacity:");
           C:     string departmentCapa = Console.ReadLine();
                int newCapacity = (int)existingDepartment.Capacity; 

                if (!string.IsNullOrWhiteSpace(departmentCapa))
                {
                    if (!int.TryParse(departmentCapa, out newCapacity) || newCapacity < 1)
                    {
                        Console.WriteLine(ValidationMessages.IncorrectCapacityFormat);
                        goto C; 
                    }
                }               
                await _departmentService.UpdateAsync(id, new Department { Name = departmentName, Capacity = newCapacity });

                Console.WriteLine("The department has been successfully updated.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }               
    }   
}

