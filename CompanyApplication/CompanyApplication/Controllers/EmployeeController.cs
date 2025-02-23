using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Azure;
using Domain.Entities;
using Microsoft.SqlServer.Server;
using Repository.Helpers.Contains;
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
                    Console.WriteLine(ValidationMessages.EmptyNameError);
                    goto Name;
                }
                if (Regex.IsMatch(employeeName, @"[\d\W_]"))
                {
                    Console.WriteLine(ValidationMessages.InvalidNameeFormat);
                    goto Name;
                }
                
                Console.WriteLine("Add Employee Surname:");
               Surname: string employeeSurname = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(employeeSurname))
                {
                    Console.WriteLine(ValidationMessages.EmptySurnameError);
                    goto Surname;
                }

                if (Regex.IsMatch(employeeSurname, @"[\d\W_]"))
                {
                    Console.WriteLine(ValidationMessages.InvalidSurnameFormat);
                    goto Surname;
                }

                Console.WriteLine("Add age :");
          Age:  string input = Console.ReadLine();
                
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine(ValidationMessages.InvalidAgeInput);
                    goto Age;
                }               
                if (Regex.IsMatch(input, @"[^\d]"))
                {
                    Console.WriteLine(ValidationMessages.AgeInputError);
                    goto Age;
                }             
                int age = int.Parse(input);

                if (age < 0)
                {
                    Console.WriteLine(ValidationMessages.AgeValidationError);
                    goto Age;
                }
                if (age < 18)
                {
                    Console.WriteLine(ValidationMessages.AgeLimitError);
                    goto Age;
                }
                if (age > 67)
                {
                    Console.WriteLine(ValidationMessages.AgeLimitExceeded);
                    goto Age;
                }

                Console.WriteLine("Add address:");
      Address:   string address = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(address))
                {
                    Console.WriteLine(ValidationMessages.EmptyAddressError);
                    goto Address;
                }
                if (Regex.IsMatch(address, @"^[^\w\s]+$"))
                {
                    Console.WriteLine(ValidationMessages.InvalidAddressFormat);
                    goto Address;
                }
                if (address.Contains("-"))
                {
                    Console.WriteLine(ValidationMessages.AddressContainsNegativeSign);
                    goto Address;
                }
                if (Regex.IsMatch(address, @"^\d+$"))
                {
                    Console.WriteLine(ValidationMessages.AddressValidationError);
                    goto Address;
                }
                Console.WriteLine("Add Department ID:");
          Id:     string departmentInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(departmentInput))
                {
                    Console.WriteLine(ValidationMessages.InvalidIDInput);
                    goto Id;
                }
                if (Regex.IsMatch(departmentInput, @"[^\d]") || int.Parse(departmentInput) <= 0)
                {
                    Console.WriteLine(ValidationMessages.InvalidDepartmentIDFormat);
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
                Console.WriteLine("Create successfully");
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message); 
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
                    Console.WriteLine(ValidationMessages.EmployeeIDValidationError);
                    goto I;
                }
                if (!int.TryParse(input, out int id))
                {
                    Console.WriteLine(ValidationMessages.InvalidEmployeeIdInput);
                    goto I;
                }

                var employee = await _employeeService.GetByIdAsync(id);

                if (employee == null)
                {
                    Console.WriteLine(ValidationMessages.EmployeeNotFoundbyID);
                    goto I;
                }
                else
                {
                    Console.WriteLine($"Id {employee.Id},Name:{employee.Name},Surname:{employee.Surname},Age:{employee.Age},Address:{employee.Address},DepartmentId:{employee.DepartmentId},CreateDate:{employee.CreateDate}");
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
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
                if (!int.TryParse(ageStr, out age))
                {
                    Console.WriteLine(ValidationMessages.Cannot);
                    goto I;
                }

                if (age < 18 || age > 67)
                {
                    Console.WriteLine(ValidationMessages.Between);
                    goto I;
                }

                var empAge = await _employeeService.GetByAgeAsync(age);

                if (empAge == null || !empAge.Any())
                {
                    Console.WriteLine(ValidationMessages.Employee);
                    goto I;
                }

                foreach (var employee in empAge)
                {
                    Console.WriteLine($"Id: {employee.Id}, Employee: {employee.Name} {employee.Surname}, Age: {employee.Age}, Address: {employee.Address}, CreateDate: {employee.CreateDate}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); 
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
                    Console.WriteLine(ValidationMessages.Invalid);
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
                    Console.WriteLine(ValidationMessages.NotFoundEmployee);
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
            
               Console.WriteLine("Add the employee name or surname: ");
            N: string nameOrSurname = Console.ReadLine()?.Trim();

                var employees = await _employeeService.GetAllAsync();

                if (employees == null || !employees.Any())
                {
                    Console.WriteLine(ValidationMessages.NoEmployeesAvailable);
                    return;
                }

                if (string.IsNullOrWhiteSpace(nameOrSurname))
                {
                    foreach (var employee in employees)
                    {
                        Console.WriteLine($"Id:{employee.Id}, Name:{employee.Name}, Surname:{employee.Surname}, Age:{employee.Age}, Address:{employee.Address}, Department: {employee.DepartmentId}, CreateDate: {employee.CreateDate}");
                    }
                    return;
                }

            
                var data = await _employeeService.SearchNameOrSurnameAsync(nameOrSurname);

                if (data == null || !data.Any())
                {
                    Console.WriteLine(ValidationMessages.EmployeeNotFoundbyNameorSurname);
                    goto N;
                }
                foreach (var employee in data)
                {
                    Console.WriteLine($"Id:{employee.Id}, Name:{employee.Name}, Surname:{employee.Surname}, Age:{employee.Age}, Address:{employee.Address}, Department: {employee.DepartmentId}, CreateDate: {employee.CreateDate}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
            
             M:   Console.WriteLine("Please enter the ID of the employee you want to update:");
                if (!int.TryParse(Console.ReadLine(), out int employeeId))
                {
                    Console.WriteLine(ValidationMessages.nvalidEmployeeIDInput);
                    goto M; 
                }

                var existingEmployee = await _employeeService.GetByIdAsync(employeeId);
                if (existingEmployee == null)
                {
                    Console.WriteLine(ValidationMessages.NotFoundEmployee);
                    goto M; 
                }

                Console.WriteLine($"Add new employee name:");
         N:     string employeeName = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(employeeName))
                {
                    employeeName = existingEmployee.Name;
                }

                if (Regex.IsMatch(employeeName, @"[\d\W_]"))
                {

                    Console.WriteLine(ValidationMessages.EmployeeNameValidationError);
                    goto N;
                }


                Console.WriteLine("Add new employee surname :");
          S:    string employeeSurname = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(employeeSurname))
                {
                    employeeSurname = existingEmployee.Surname;
                }

                if (Regex.IsMatch(employeeSurname, @"[\d\W_]"))
                {
                    Console.WriteLine(ValidationMessages.EmployeeSurnameValidationError);
                    goto S; 
                }

                Console.WriteLine("Add new employee age");
           I:   string input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    input = existingEmployee.Age.ToString();
                }

                if (Regex.IsMatch(input, @"[^\d]"))
                {
                    Console.WriteLine("Age must consist of numbers only.");
                    goto I;
                }

                int age = int.Parse(input);
                if (age < 0 || age < 18 || age > 95)
                {
                    Console.WriteLine(ValidationMessages.Invalidd);
                    goto I; 
                }

                Console.WriteLine("Add new  employee Address:");
          A:   string address = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(address))
                {
                    address = existingEmployee.Address;
                }

                if (Regex.IsMatch(address, @"^[^\w\s]+$") || address.Contains("-"))
                {
                    Console.WriteLine("The address format is incorrect.");
                    goto A;
                }

                Console.WriteLine("Add new department ID:");
                string departmentInput = Console.ReadLine();
                int departmentId = existingEmployee.DepartmentId;
                if (!string.IsNullOrWhiteSpace(departmentInput))
                {
                    departmentId = int.Parse(departmentInput);
                    var department = await _departmentService.GetByIdAsync(departmentId);
                    if (department == null)
                    {
                        Console.WriteLine(ValidationMessages.DepartmentNotFoundbyID);
                        goto M; 
                    }
                }

                var employee = new Employee { Name = employeeName, Surname = employeeSurname, Age = age, Address = address, DepartmentId = departmentId };
                await _employeeService.UpdateAsync(employeeId, employee);
                Console.WriteLine("The employee information has been successfully updated.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public async Task GetAllDepartmentByNameAsync()
        {
            try
            {
                Console.WriteLine("Add department name:");
            N:  string departmentName = Console.ReadLine();

                var employees = await _employeeService.GetAllDepartmentByNameAsync(departmentName);
                if (string.IsNullOrWhiteSpace(departmentName) || Regex.IsMatch(departmentName, @"[\d\W_]|^-"))
                {
                    Console.WriteLine(ValidationMessages.DepartmentNameValidationError);
                    goto N;
                }
               
                var department = await _employeeService.GetAllDepartmentByNameAsync(departmentName);
                if (department == null)
                {
                    Console.WriteLine(ValidationMessages.DepartmentNotFoundbyName);
                    goto N;
                  
                }
                if (employees == null || !employees.Any())
                {
                    Console.WriteLine(ValidationMessages.NoEmployeesFound);
                    goto N;
                   
                }
                if (employees != null && employees.Any())
                {
                    Console.WriteLine($"Employees in the department:");
                    foreach (var employee in employees)
                    {
                        Console.WriteLine($"Id;{employee.Id},Employee: {employee.Name} {employee.Surname}, Age: {employee.Age}, Address: {employee.Address},CreateDate:{employee.CreateDate}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
                    Console.WriteLine(ValidationMessages.InvalidInput);
                    goto ID;
                }

                var employees = await _employeeService.GetDepartmentById(departmentId);
              
                if (employees == null || !employees.Any())
                {
                    Console.WriteLine(ValidationMessages.EmployeeLookupError);
                    goto ID;
                }

                foreach (var emp in employees)
                {
                    Console.WriteLine($"Id: {emp.Id}, Name: {emp.Name}, Surname: {emp.Surname}, Age: {emp.Age}, Address: {emp.Address}, DepartmentId: {emp.DepartmentId}, CreateDate: {emp.CreateDate}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
