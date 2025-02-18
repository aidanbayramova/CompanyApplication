using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

                Console.WriteLine("Add Department Capacity");
                int isCapacityValid = int.Parse(Console.ReadLine());
                int departmentCapacity;
                if (int.TryParse(isCapacityValid, out departmentCapacity))
                {
                    if (departmentCapacity >= 0)
                    {
                        isCapacityValid = true;
                    }
                    else
                    {
                        Console.WriteLine("Department kapasitesi mənfi ola bilməz. Zəhmət olmasa, yenidən cəhd edin.");
                    }
                }
                else
                {
                    Console.WriteLine("Zəhmət olmasa, kapasite üçün düzgün bir rəqəm daxil edin.");
                }

                var department = new Department { Name = departmentName, Capacity = departmentCapacity };
                await _departmentService.CreateAsync(department);
                Console.WriteLine("Department Created Successfully");
            }
            catch (Exception ex)
            {

                Console.WriteLine("An error occurred while creating department.");
            }

        }
    }   
}
