using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Azure;
using Domain.Entities;
using Service.Services;
using Service.Services.Interfaces;

namespace CompanyApplication.Controllers
{
    public class EmployeeController
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;

        public EmployeeController()
        {
            _employeeService = new EmployeeService();
            _departmentService = new DepartmentService();
        }
        public async Task CreateAsync()
        {
            try
            {
                Console.WriteLine("Add Employee Name:");
          Name: string employeeName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(employeeName))
                {
                    Console.WriteLine("Employenin adı boş ola bilməz. Zəhmət olmasa, yenidən cəhd edin.");
                    goto Name;
                }
                if (Regex.IsMatch(employeeName, @"[\d\W_]"))
                {
                    Console.WriteLine("Employenin adı rəqəm və ya xüsusi işarələr (məsələn, @, #, &, ...) ehtiva edə bilməz. Zəhmət olmasa, yenidən cəhd edin.");
                    goto Name;
                }
                
                Console.WriteLine("Add Employee Surname:");
               Surname: string employeeSurname = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(employeeSurname))
                {
                    Console.WriteLine("Employenin soyadi boş ola bilməz. Zəhmət olmasa, yenidən cəhd edin.");
                    goto Surname;
                }

                if (Regex.IsMatch(employeeSurname, @"[\d\W_]"))
                {
                    Console.WriteLine("Employenin soyadi rəqəm və ya xüsusi işarələr (məsələn, @, #, &, ...) ehtiva edə bilməz. Zəhmət olmasa, yenidən cəhd edin.");
                    goto Surname;
                }

                Console.WriteLine("Add age :");
          Age:  string input = Console.ReadLine();
                
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Yaş boş ola bilməz. Zəhmət olmasa, düzgün bir yaş daxil edin.");
                    goto Age;
                }               
                if (Regex.IsMatch(input, @"[^\d]"))
                {
                    Console.WriteLine("Yaş yalnız rəqəmlərdən ibarət olmalıdır. Zəhmət olmasa, düzgün bir yaş daxil edin.");
                    goto Age;
                }             
                int age = int.Parse(input);

                if (age < 0)
                {
                    Console.WriteLine("Yaş mənfi ola bilməz. Zəhmət olmasa, düzgün bir yaş daxil edin.");
                    goto Age;
                }
                if (age < 18)
                {
                    Console.WriteLine("Employee-nin yaşı 18-dən böyük olmalıdır. Zəhmət olmasa, düzgün bir yaş daxil edin.");
                    goto Age;
                }
                if (age > 95)
                {
                    Console.WriteLine("Yaş 95-dən böyük ola bilməz. Zəhmət olmasa, düzgün bir yaş daxil edin.");
                    goto Age;
                }

                Console.WriteLine("Add address:");
      Address:   string address = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(address))
                {
                    Console.WriteLine("Adres boş ola bilməz. Zəhmət olmasa, düzgün bir adres daxil edin.");
                    goto Address;
                }
                if (Regex.IsMatch(address, @"^[^\w\s]+$"))
                {
                    Console.WriteLine("Adres yalnız xüsusi işarələrdən ibarət ola bilməz. Zəhmət olmasa, düzgün bir adres daxil edin.");
                    goto Address;
                }
                if (address.Contains("-"))
                {
                    Console.WriteLine("Adres mənfi işarə (-) ehtiva edə bilməz. Zəhmət olmasa, düzgün bir adres daxil edin.");
                    goto Address;
                }
                if (Regex.IsMatch(address, @"^\d+$"))
                {
                    Console.WriteLine("Adres yalnız rəqəmlərdən ibarət ola bilməz. Zəhmət olmasa, düzgün bir adres daxil edin.");
                    goto Address;
                }
                Console.WriteLine("Add Department ID:");
          Id:     string departmentInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(departmentInput))
                {
                    Console.WriteLine("Department ID boş ola bilməz. Zəhmət olmasa, düzgün bir ID daxil edin.");
                    goto Id;
                }
                if (Regex.IsMatch(departmentInput, @"[^\d]") || int.Parse(departmentInput) <= 0)
                {
                    Console.WriteLine("Department ID yalnız müsbət tam ədədlərdən ibarət olmalıdır.");
                    goto Id;
                }              
                int departmentId = int.Parse(departmentInput);
                 Employee employee = new Employee()
                 {
                     Name = employeeName,
                     Surname = employeeSurname,
                     Age = age,
                     Address= address,
                     DepartmentId=departmentId
                 };
                await _employeeService.CreateAsync(employee);
                Console.WriteLine("create successfully");
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Gözlənilməz xəta baş verdi: {ex.Message}"); 
            }

        }

        public async Task GetByIdAsync()
        {
            try
            {
                Console.WriteLine("Add employee Id :");
           I:     string input = Console.ReadLine();

                if (Regex.IsMatch(input, @"[^\d]") || input.StartsWith("-"))
                {
                    Console.WriteLine("Employee Id yalnız müsbət rəqəm olmalıdır və mənfi işarə və xüsusi simvollar daxil edilə bilməz.");
                    goto I;
                }
                if (!int.TryParse(input, out int id))
                {
                    Console.WriteLine("Zəhmət olmasa, düzgün bir Employee Id daxil edin.");
                    goto I;
                }

                var employee = await _employeeService.GetByIdAsync(id);

                if (employee == null)
                {
                    Console.WriteLine($"id {id} olan employee tapılmadı.");
                    goto I;
                }
                else
                {
                    Console.WriteLine($"Id {employee.Id},Name:{employee.Name},Surname:{employee.Surname},Age:{employee.Age},Address:{employee.Address},DepartmentId:{employee.DepartmentId},CreateDate:{employee.CreateDate}");
                }

            }
            catch (Exception)
            {

                Console.WriteLine("gozlenilmez  xeta");
            }
        }

        public async Task GetAllAsync()
        {
            var employees = await _employeeService.GetAllAsync();
            foreach (var employee in employees)
            {
                Console.WriteLine($"Id:{employee.Id},Name:{employee.Name},Surname:{employee.Surname},Age:{employee.Age},Address:{employee.Address}, DepartmentId : {employee.DepartmentId}, Createdate:{employee.CreateDate}");
            }
        }

        public async Task GetByAgeAsync()
        {
            try
            {
            I: Console.WriteLine("Add employee age:");
                string ageStr = Console.ReadLine();
                int age = 1;

                // Yaşın düzgün formatda daxil edilib-edilmədiyini yoxlayırıq
                if (!int.TryParse(ageStr, out age))
                {
                    Console.WriteLine("Yaş boş ola bilməz. Zəhmət olmasa, düzgün bir yaş daxil edin.");
                    goto I;
                }

                // Yaşın 18-95 aralığında olmasını təmin edirik
                if (age < 18 || age > 95)
                {
                    Console.WriteLine("Yaş yalnız 18-95 aralığında olan müsbət rəqəm olmalıdır.");
                    goto I;
                }

                // Həmin yaşa aid işçiləri tapırıq
                var empAge = await _employeeService.GetByAgeAsync(age);

                // İşçi tapılmadıqda istifadəçiyə məlumat veririk
                if (empAge == null || !empAge.Any())
                {
                    Console.WriteLine($"Yaşı {age} olan işçi tapılmadı. Yenidən yaş daxil edin.");
                    goto I;
                }

                // İşçiləri ekranda göstəririk
                foreach (var employee in empAge)
                {
                    Console.WriteLine($"Id: {employee.Id}, Employee: {employee.Name} {employee.Surname}, Age: {employee.Age}, Address: {employee.Address}, CreateDate: {employee.CreateDate}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("gozlenilmez xeta"); 
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
                var department = await _employeeService.GetByIdAsync(id);
                if (department != null)
                {
                    await _employeeService.DeleteAsync(id);
                    Console.WriteLine("Deleted Successfully");
                }
                else
                {
                    Console.WriteLine("employee not found");
                    goto Id;
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }

        //public async Task SearchNameOrSurnameAsync()
        //{
        //    try
        //    {
        //        Console.WriteLine("Add the employee name or surname : ");
        //     N:   string nameOrSurname = Console.ReadLine();

        //        var employees = await _employeeService.GetAllAsync();


        //        if (employees == null || !employees.Any())
        //        {
        //            Console.WriteLine($"Adı '{nameOrSurname}' olan heç bir departament tapılmadı.");
        //            goto N;
        //        }            

        //        var data = await _employeeService.SearchNameOrSurnameAsync(nameOrSurname);
        //        foreach (var employee in data)
        //        {
        //            Console.WriteLine($"Id:{employee.Id},Name:{employee.Name},Surnamr:{employee.Surname},Age:{employee.Age},Address:{employee.Address}, Department : {employee.DepartmentId}, Createdate:{employee.CreateDate}");
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error searching for employee: {ex.Message}");
        //    }
        //}


        public async Task SearchNameOrSurnameAsync()
        {
            try
            {
            
               Console.WriteLine("Add the employee name or surname: ");
            N: string nameOrSurname = Console.ReadLine()?.Trim(); // Trim() boşluqları silir

                var employees = await _employeeService.GetAllAsync();

                // Əgər sistemdə ümumiyyətlə işçi yoxdursa
                if (employees == null || !employees.Any())
                {
                    Console.WriteLine("Sistemdə heç bir işçi yoxdur.");
                    return;
                }

                // Əgər daxil edilən dəyər boş və ya sadəcə space-dirsə, bütün işçiləri göstər
                if (string.IsNullOrWhiteSpace(nameOrSurname))
                {
                    foreach (var employee in employees)
                    {
                        Console.WriteLine($"Id:{employee.Id}, Name:{employee.Name}, Surname:{employee.Surname}, Age:{employee.Age}, Address:{employee.Address}, Department: {employee.DepartmentId}, CreateDate: {employee.CreateDate}");
                    }
                    return;
                }

                // Axtarış et
                var data = await _employeeService.SearchNameOrSurnameAsync(nameOrSurname);

                // Əgər heç bir nəticə tapılmayıbsa
                if (data == null || !data.Any())
                {
                    Console.WriteLine($"Adı və ya soyadı '{nameOrSurname}' olan heç bir işçi tapılmadı. Yenidən cəhd et:");
                    goto N;
                }

                // Tapılan işçiləri ekrana çıxar
                foreach (var employee in data)
                {
                    Console.WriteLine($"Id:{employee.Id}, Name:{employee.Name}, Surname:{employee.Surname}, Age:{employee.Age}, Address:{employee.Address}, Department: {employee.DepartmentId}, CreateDate: {employee.CreateDate}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error searching for employee: {ex.Message}");
            }
        }
        public async Task GetAllCountAsync()
        {
           var employeeCount = await _employeeService.GetAllCountAsync();
            Console.WriteLine(employeeCount);
                       
        }
        public async Task UpdateAsync()
        {
            try
            {
            Start:
                Console.WriteLine("Yeniləmək istədiyiniz employee-in ID-sini daxil edin:");
                if (!int.TryParse(Console.ReadLine(), out int employeeId))
                {
                    Console.WriteLine("Zəhmət olmasa, düzgün employee ID-si daxil edin.");
                    goto Start; // Yanlış ID daxil edildikdə yenidən soruşuruq
                }

                var existingEmployee = await _employeeService.GetByIdAsync(employeeId);
                if (existingEmployee == null)
                {
                    Console.WriteLine("Employee tapılmadı.");
                    goto Start; // Employee tapılmadıqda yenidən soruşuruq
                }

                Console.WriteLine($"Yeni Employee Adını daxil edin (Cari Ad: {existingEmployee.Name}):");
         N:     string employeeName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(employeeName))
                {
                    employeeName = existingEmployee.Name;
                }

                if (Regex.IsMatch(employeeName, @"[\d\W_]"))
                {

                    Console.WriteLine("Employee adı rəqəm və ya xüsusi işarələr (məsələn, @, #, &, ...) ehtiva edə bilməz.");
                    goto N; // Yanlış format daxil edildikdə yenidən soruşuruq
                }


                Console.WriteLine("Yeni Employee soyadını daxil et:");
          S:    string employeeSurname = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(employeeSurname))
                {
                    employeeSurname = existingEmployee.Surname;
                }

                if (Regex.IsMatch(employeeSurname, @"[\d\W_]"))
                {
                    Console.WriteLine("Employee soyadı rəqəm və ya xüsusi işarələr (məsələn, @, #, &, ...) ehtiva edə bilməz.");
                    goto S; // Yanlış format daxil edildikdə yenidən soruşuruq
                }

                Console.WriteLine("Yeni yaşı daxil et:");
           I:   string input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    input = existingEmployee.Age.ToString();
                }

                if (Regex.IsMatch(input, @"[^\d]"))
                {
                    Console.WriteLine("Yaş yalnız rəqəmlərdən ibarət olmalıdır.");
                    goto I; // Yanlış format daxil edildikdə yenidən soruşuruq
                }

                int age = int.Parse(input);
                if (age < 0 || age < 18 || age > 95)
                {
                    Console.WriteLine("Yaş düzgün deyil. Zəhmət olmasa, düzgün yaş daxil edin.");
                    goto I; // Yanlış yaş daxil edildikdə yenidən soruşuruq
                }

                Console.WriteLine("Yeni adresi daxil et:");
          A:   string address = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(address))
                {
                    address = existingEmployee.Address;
                }

                if (Regex.IsMatch(address, @"^[^\w\s]+$") || address.Contains("-"))
                {
                    Console.WriteLine("Adresin formatı səhvdir.");
                    goto A; // Yanlış adres daxil edildikdə yenidən soruşuruq
                }

                Console.WriteLine("Yeni departament ID-sini daxil et:");
                string departmentInput = Console.ReadLine();
                int departmentId = existingEmployee.DepartmentId;
                if (!string.IsNullOrWhiteSpace(departmentInput))
                {
                    departmentId = int.Parse(departmentInput);
                    var department = await _departmentService.GetByIdAsync(departmentId);
                    if (department == null)
                    {
                        Console.WriteLine($"Departament ID-si {departmentId} ilə tapılmadı.");
                        goto Start; // Yanlış departament ID-si daxil edildikdə yenidən soruşuruq
                    }
                }

                var employee = new Employee { Name = employeeName, Surname = employeeSurname, Age = age, Address = address, DepartmentId = departmentId };
                await _employeeService.UpdateAsync(employeeId, employee);
                Console.WriteLine("İşçi məlumatları uğurla yeniləndi.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Xəta baş verdi: {ex.Message}");
            }
        }

        public async Task GetAllDepartmentByNameAsync()
        {
            try
            {
                Console.WriteLine("Departament adını daxil edin:");
            N:  string departmentName = Console.ReadLine();

                var employees = await _employeeService.GetAllDepartmentByNameAsync(departmentName);
                if (string.IsNullOrWhiteSpace(departmentName) || Regex.IsMatch(departmentName, @"[\d\W_]|^-"))
                {
                    Console.WriteLine("Departament adı yalnız hərflərdən ibarət olmalıdır və rəqəm, mənfi ədəd, və xüsusi simvollar daxil edilməməlidir.");
                    goto N;
                }

                
                var department = await _employeeService.GetAllDepartmentByNameAsync(departmentName);
                if (department == null)
                {
                    Console.WriteLine($"'{departmentName}' adlı departament tapılmadı.");
                    goto N;
                  
                }

                

                if (employees == null || !employees.Any())
                {
                    Console.WriteLine($"'{departmentName}' departamentində heç bir işçi yoxdur.");
                    goto N;
                   
                }


                if (employees != null && employees.Any())
                {
                    Console.WriteLine($"'{departmentName}' departamentinə aid işçilər:");
                    foreach (var employee in employees)
                    {
                        Console.WriteLine($"Id;{employee.Id},Employee: {employee.Name} {employee.Surname}, Age: {employee.Age}, Address: {employee.Address},CreateDate:{employee.CreateDate}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Xəta baş verdi: {ex.Message}");
            }
        }
        public async Task GetDepartmentById()
        {
            try
            {
            ID:
                Console.WriteLine("Add Department Id: ");
                if (!int.TryParse(Console.ReadLine(), out int departmentId) || departmentId <= 0)
                {
                    Console.WriteLine("Invalid Department Id. Zəhmət olmasa düzgün bir ID daxil edin.");
                    goto ID;
                }

                var employees = await _employeeService.GetDepartmentById(departmentId);

                // Bu hissə daxilində yoxlayırıq ki, tapılan işçilər null deyil və hər hansı işçi mövcuddur
                if (employees == null || !employees.Any())
                {
                    Console.WriteLine($"Bu {departmentId} ID ilə bağlı heç bir işçi tapılmadı. Zəhmət olmasa düzgün bir departament ID daxil edin.");
                    goto ID;
                }

                foreach (var emp in employees)
                {
                    Console.WriteLine($"Id: {emp.Id}, Name: {emp.Name}, Surname: {emp.Surname}, Age: {emp.Age}, Address: {emp.Address}, DepartmentId: {emp.DepartmentId}, CreateDate: {emp.CreateDate}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving employees: {ex.Message}");
            }
        }
    }
}
