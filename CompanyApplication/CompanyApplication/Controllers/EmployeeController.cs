using System;
using System.Collections.Generic;
using System.Linq;
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
            //_employeeService = new IEmployeeService();
        }
        public async Task CreateAsync()
        {
            try
            {
                Console.WriteLine("Add Employee Name:");
                string employeeName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(employeeName))
                {
                    Console.WriteLine("Employenin adı boş ola bilməz. Zəhmət olmasa, yenidən cəhd edin.");
                }
                if (Regex.IsMatch(employeeName, @"[\d\W_]"))
                {
                    Console.WriteLine("Employenin adı rəqəm və ya xüsusi işarələr (məsələn, @, #, &, ...) ehtiva edə bilməz. Zəhmət olmasa, yenidən cəhd edin.");
                    return;
                }
                if (employeeName == null)
                {
                    Console.WriteLine("Employenin məlumatları boş ola bilməz.");
                }

                Console.WriteLine("Add Employee Surname:");
                string employeeSurname = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(employeeSurname))
                {
                    Console.WriteLine("Employenin soyadi boş ola bilməz. Zəhmət olmasa, yenidən cəhd edin.");
                }

                if (Regex.IsMatch(employeeSurname, @"[\d\W_]"))
                {
                    Console.WriteLine("Employenin soyadi rəqəm və ya xüsusi işarələr (məsələn, @, #, &, ...) ehtiva edə bilməz. Zəhmət olmasa, yenidən cəhd edin.");
                    return;
                }

                if (employeeSurname == null)
                {
                    Console.WriteLine("Employenin məlumatları boş ola bilməz.");
                }

                Console.WriteLine("Add age :");
                string input = Console.ReadLine();
                
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Yaş boş ola bilməz. Zəhmət olmasa, düzgün bir yaş daxil edin.");
                    return;
                }               
                if (Regex.IsMatch(input, @"[^\d]"))
                {
                    Console.WriteLine("Yaş yalnız rəqəmlərdən ibarət olmalıdır. Zəhmət olmasa, düzgün bir yaş daxil edin.");
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

                Console.WriteLine("Add address:");
                string address = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(address))
                {
                    Console.WriteLine("Adres boş ola bilməz. Zəhmət olmasa, düzgün bir adres daxil edin.");
                    return;
                }
                if (Regex.IsMatch(address, @"^[^\w\s]+$"))
                {
                    Console.WriteLine("Adres yalnız xüsusi işarələrdən ibarət ola bilməz. Zəhmət olmasa, düzgün bir adres daxil edin.");
                    return;
                }
                if (address.Contains("-"))
                {
                    Console.WriteLine("Adres mənfi işarə (-) ehtiva edə bilməz. Zəhmət olmasa, düzgün bir adres daxil edin.");
                    return;
                }

                Console.WriteLine("create successfully");
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Gözlənilməz xəta baş verdi: {ex.Message}"); 
            }

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

    }
}
