using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Repository.Migrations;

namespace Repository.Helpers.Enum
{
    public enum OperationTypes
    {
        CreateDepartment = 1,
        GetAllDepartments,
        UpdateDepartment,
        DeleteDepartment,
        GetDepartmentById,
        SearchDepartmentsByName,
        CreateEmployees,
        GetAllEmployees,
        UpdateEmployees,
        GetEmployeeById,
        DeleteEmployee,
        GetEmployeesByAge,
        GetEmployeesByDepartmentId,
        GetDepartmentName,
        SearchEmployeeByNameOrSurname,
        GetAllEmployeesCount

    }
}
