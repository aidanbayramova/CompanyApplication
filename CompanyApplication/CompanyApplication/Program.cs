using CompanyApplication.Controllers;
using Repository.Helpers.Enum;
while (true)
{
    DepartmentController departmentController = new DepartmentController();
    EmployeeController employeeController = new EmployeeController();
    // UserController userController = new UserController();

    Console.WriteLine("  1-CreateDepartment ;  2-GetAllDepartments  ;  3-UpdateDepartment  ;   4-DeleteDepartment  ;" +
        "  5-GetDepartmentById  ;   6-SearchDepartmentsByName  7-CreateEmployees  ;   8-GetAllEmployees  ;  9-UpdateEmployees  ; " +
        " 10-GetEmployeeById  ;  11-DeleteEmployee  ; 12- GetEmployeesByAge  ; 13-GetEmployeesByDepartmentId    ;  14-GetDepartmentName" +
        "   15-SearchEmployeeByNameOrSurname   ; 16-GetAllEmployeesCount   ");

  Operation: string operation = Console.ReadLine();

    bool isCorrectOperation = int.TryParse(operation, out int selectedOperation);

    if (isCorrectOperation)
    {
        switch (selectedOperation)
        {
            case(int)OperationTypes.CreateDepartment:
                await departmentController.CreateAsync();
                break;
            case (int)OperationTypes.GetAllDepartments:
                await departmentController.GetAllAsync();
                break;
            case (int)OperationTypes.UpdateDepartment:
                await departmentController.UpdateAsync();
                break;
            case (int)OperationTypes.DeleteDepartment:
                await departmentController.DeleteAsync();
                break;
            case (int)OperationTypes.GetDepartmentById:
                await departmentController.GetByIdAsync();
                break;
            case (int)OperationTypes.SearchDepartmentsByName:
                await departmentController.SearchAsync();
                break;
            case (int)OperationTypes.CreateEmployees:
                await employeeController.CreateAsync();
                break;
            case (int)OperationTypes.GetAllEmployees:
                await employeeController.GetAllAsync();
                break;
            case (int)OperationTypes.UpdateEmployees:
                await employeeController.UpdateAsync();
                break;
            case (int)OperationTypes.GetEmployeeById:
                await employeeController.GetByIdAsync();
                break;
            case (int)OperationTypes.DeleteEmployee:
                await employeeController.DeleteAsync();
                break;
            case (int)OperationTypes.GetEmployeesByAge:
                await employeeController.GetByAgeAsync();
                break;
            case (int)OperationTypes.GetEmployeesByDepartmentId:
               await employeeController.GetDepartmentById();
               break;
            case (int)OperationTypes.GetDepartmentName:
                await employeeController.GetAllDepartmentByNameAsync();
                break;
            case (int)OperationTypes.SearchEmployeeByNameOrSurname:
                await employeeController.SearchNameOrSurnameAsync();
                break;
            case (int)OperationTypes.GetAllEmployeesCount:
                await employeeController.GetAllCountAsync();
                break;
            default:
                Console.WriteLine("Operation is wrong , please select again");
                goto Operation;
        }
    }
    else
    {
        Console.WriteLine("Operation format is wrong , select operation again");
        goto Operation;
    }

}




