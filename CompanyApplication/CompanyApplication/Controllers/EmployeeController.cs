using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Service.Services;
using Service.Services.Interfaces;

namespace CompanyApplication.Controllers
{
    public class EmployeeController
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController()
        {
            _employeeService = new EmployeeController();
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

            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}
