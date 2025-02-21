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

        public EmployeeController()
        {
            _employeeService = new EmployeeService();
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
                    Console.WriteLine($"Id{employee.Id},{employee.Name},{employee.Surname},{employee.Age},{employee.Address},{employee.DepartmentId},{employee.CreateDate}");
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
                Console.WriteLine($"Id:{employee.Id},Name:{employee.Name},Surnamr:{employee.Surname},Age:{employee.Age},Address:{employee.Address}, Department : {employee.DepartmentId}, Createdate:{employee.CreateDate}");
            }
        }

        public async Task GetByAgeAsync()
        {
            try
            {
            I: Console.WriteLine("Add employee age:");
                string ageStr = Console.ReadLine();
                int age = 1;
                if (!int.TryParse(ageStr, out age))
                {
                    Console.WriteLine("Yaş boş ola bilməz. Zəhmət olmasa, düzgün bir yaş daxil edin.");
                    goto I;
                }
                //if (string.IsNullOrWhiteSpace(ageStr))
                //{
                //    Console.WriteLine("Yaş boş ola bilməz. Zəhmət olmasa, düzgün bir yaş daxil edin.");
                //    goto I;
                //}
                if (!int.TryParse(ageStr, out age) || age < 18 || age > 95)
                {
                    Console.WriteLine("Yaş yalnız 18-95 aralığında olan müsbət rəqəm olmalıdır.");
                    goto I;
                }
                if (ageStr == null || !ageStr.Any())
                {
                    Console.WriteLine($"Yaşı {age} olan employee tapılmadı.");
                    return;
                }
                var empAge = await _employeeService.GetByAgeAsync(age);
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

        public async Task SearchNameOrSurnameAsync()
        {
            try
            {
                Console.WriteLine("Add the employee name or surname : ");
             N:   string nameOrSurname = Console.ReadLine();

                var employees = await _employeeService.GetAllAsync();

                if (string.IsNullOrWhiteSpace(nameOrSurname))
                {
                    foreach (var employee in employees)
                    {
                        Console.WriteLine($"Id:{employee.Id},Name:{employee.Name},Surnamr:{employee.Surname},Age:{employee.Age},Address:{employee.Address}, Department : {employee.DepartmentId}, Createdate:{employee.CreateDate}");
                    
                    }

                }
                if (employees == null || !employees.Any())
                {
                    Console.WriteLine($"Adı '{nameOrSurname}' olan heç bir departament tapılmadı.");
                    goto N;
                }            

                var data = await _employeeService.SearchNameOrSurnameAsync(nameOrSurname);
                foreach (var employee in data)
                {
                    Console.WriteLine($"Id:{employee.Id},Name:{employee.Name},Surnamr:{employee.Surname},Age:{employee.Age},Address:{employee.Address}, Department : {employee.DepartmentId}, Createdate:{employee.CreateDate}");
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
                Console.WriteLine("Yeniləmək istədiyiniz employeenin ID-sini daxil edin:");
                if (!int.TryParse(Console.ReadLine(), out int employeeId))
                {
                    Console.WriteLine("Zəhmət olmasa, düzgün employee ID-si daxil edin.");
                    return;
                }
                var existingEmployee = await _employeeService.GetByIdAsync(employeeId);
                if (existingEmployee == null)
                {
                    Console.WriteLine("Departament tapılmadı.");
                    return;
                }

                Console.WriteLine($"Yeni Employee Adını daxil edin (Cari Ad: {existingEmployee.Name}):");
                string employeeName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(employeeName))
                {
                    employeeName = existingEmployee.Name;
                }

                if (Regex.IsMatch(employeeName, @"[\d\W_]"))
                {
                    Console.WriteLine("Employenin adı rəqəm və ya xüsusi işarələr (məsələn, @, #, &, ...) ehtiva edə bilməz.");
                    return;
                }
                Console.WriteLine("Yeni Empoyee soyadini daxil et:");
                string employeeSurname = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(employeeSurname))
                {
                    employeeSurname = existingEmployee.Surname;
                }
                if (Regex.IsMatch(employeeSurname, @"[\d\W_]"))
                {
                    Console.WriteLine("Employenin soyadi rəqəm və ya xüsusi işarələr (məsələn, @, #, &, ...) ehtiva edə bilməz.");
                    return;
                }
                Console.WriteLine("Yeni yas elave et:");
                string input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    input = existingEmployee.Age.ToString();
                }

                if (Regex.IsMatch(input, @"[^\d]"))
                {
                    Console.WriteLine("Yaş yalnız rəqəmlərdən ibarət olmalıdır.");
                    return;
                }
                int age = int.Parse(input);
                if (age < 0)
                {
                    Console.WriteLine("Yaş mənfi ola bilməz. Zəhmət olmasa, düzgün bir yaş daxil edin.");
                    return;
                }
                if (age < 18)
                {
                    Console.WriteLine("Employee-nin yaşı 18-dən böyük olmalıdır. Zəhmət olmasa, düzgün bir yaş daxil edin.");
                    return;
                }
                if (age > 95)
                {
                    Console.WriteLine("Yaş 150-dən böyük ola bilməz. Zəhmət olmasa, düzgün bir yaş daxil edin.");
                    return;
                }
                Console.WriteLine("Yeni adresi elave ett:");
                string address = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(address))
                {
                    address = existingEmployee.Address;
                }
                if (Regex.IsMatch(address, @"^[^\w\s]+$"))
                {
                    Console.WriteLine("Adres yalnız xüsusi işarələrdən ibarət ola bilməz.");
                    return;
                }
                if (address.Contains("-"))
                {
                    Console.WriteLine("Adres mənfi işarə (-) ehtiva edə bilməz.");
                    return;
                }
                Console.WriteLine("Yeni depatment idsini elave ett");
                string departmentInput = Console.ReadLine();
                int departmentId = existingEmployee.DepartmentId;
                if (!string.IsNullOrWhiteSpace(departmentInput))
                {
                    departmentId = int.Parse(departmentInput);
                    var department = await _employeeService.GetByIdAsync(departmentId);
                    if (department == null)
                    {
                        Console.WriteLine($"Departament ID-si {departmentId} ilə tapılmadı.");
                        return;
                    }
                }
                var employee = new Employee { Name = employeeName, Surname = employeeSurname, Age = age, Address = address, DepartmentId = departmentId };
                await _employeeService.UpdateAsync(employeeId, employee);
                Console.WriteLine("İşçi məlumatları uğurla yeniləndi.");
            }
            catch (Exception)
            {

                Console.WriteLine($"Xəta baş verdi:");
            }
        }

        public async Task GetAllDepartmentByNameAsync()
        {
            try
            {
                Console.WriteLine("Departament adını daxil edin:");
                string departmentName = Console.ReadLine();

                var employees = await _employeeService.GetAllDepartmentByNameAsync(departmentName);
                if (string.IsNullOrWhiteSpace(departmentName) || Regex.IsMatch(departmentName, @"[\d\W_]|^-"))
                {
                    Console.WriteLine("Departament adı yalnız hərflərdən ibarət olmalıdır və rəqəm, mənfi ədəd, və xüsusi simvollar daxil edilməməlidir.");
                }

                
                var department = await _employeeService.GetAllDepartmentByNameAsync(departmentName);
                if (department == null)
                {
                    Console.WriteLine($"'{departmentName}' adlı departament tapılmadı.");
                  
                }

                

                if (employees == null || !employees.Any())
                {
                    Console.WriteLine($"'{departmentName}' departamentində heç bir işçi yoxdur.");
                   
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
            ID: Console.WriteLine("Add Department Id: ");
                if (!int.TryParse(Console.ReadLine(), out int departmentId) || departmentId <= 0)
                {
                    Console.WriteLine("Invalid Department Id.");
                    goto ID;
                }

                var employees = await _employeeService.GetDepartmentById(departmentId);
                if (employees == null)
                {
                    Console.WriteLine($"bele  employee tapılmadı.");
                    goto ID;
                }

                foreach (var emp in employees)
                {
                    Console.WriteLine($"Id:{emp.Id}, Name:{emp.Name}, Surname{emp.Surname},Age: {emp.Age}, address:{emp.Address}, DepartmentId:{emp.DepartmentId}, Createdate: {emp.CreateDate}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving employees: {ex.Message}");
            }
        }

    }
}
