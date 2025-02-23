using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Azure;
using Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace Repository.Helpers.Contains
{
    public class ValidationMessages
    {
        public const string InputError = "The department name cannot be empty. Please try again.";
        public const string InvalidDepartmentName = "The department name cannot contain numbers or special . Please try again.";
        public const string NameConflictError = $"A department named  already exists. Please enter a different name.";
        public const string CapacityValidationError = "Please enter a valid number for the capacity.";
        public const string NumericInputRequired = "Capacity must consist of numbers only. Please try again.";
        public const string PositiveNumberRequired = "The department capacity must be a positive number greater than 0. Please try again.";
        public const string FailedtoCreateDepartment = "An error occurred while creating department.";
        public const string InvalidIDFormat = "Invalid input. Please enter a valid number.";
        public const string NotFound = "Department not found";
        public const string NoDepartmentsFound = "There are no departments in the system.";
        public const string DepartmentNotFoundError = $"Department not found. Please try again.";
        public const string InvalidDepartmentId = "The department Id must be a positive number, and negative signs or special characters cannot be included.";
        public const string InvalidIdFormat = "Please enter a valid Department Id.";
        public const string InvalidDepartmentID = $"Department with the given ID was not found.";
        public const string InvalidIDFormatt = "Please enter a valid department ID.";
        public const string NameConflictErrorr = $"already exists, please enter a different name.";
        public const string IncorrectCapacityFormat = "The capacity has been entered in an incorrect format.";
        public const string EmptyNameError = "The employee's name cannot be empty. Please try again.";
        public const string InvalidNameeFormat = "The employee's name cannot contain numbers or special characters . Please try again.";
        public const string EmptySurnameError = "The employee's surname cannot be empty. Please try again.";
        public const string InvalidSurnameFormat = "The employee's surname cannot contain numbers or special characters . Please try again.";
        public const string InvalidAgeInput = "Age cannot be empty. Please enter a valid age.";
        public const string AgeInputError = "Age must consist of numbers only. Please enter a valid age.";
        public const string AgeValidationError = "Age cannot be negative. Please enter a valid age.";
        public const string AgeLimitError = "The employee's age must be greater than 18. Please enter a valid age.";
        public const string AgeLimitExceeded = "Age cannot be greater than 67. Please enter a valid age.";
        public const string EmptyAddressError = "The address cannot be empty. Please enter a valid address.";
        public const string InvalidAddressFormat = "The address cannot consist of special characters only. Please enter a valid address.";
        public const string AddressContainsNegativeSign = "The address cannot contain a negative sign (-). Please enter a valid address.";
        public const string AddressValidationError = "Adres yalnız rəqəmlərdən ibarət ola bilməz. Zəhmət olmasa, düzgün bir adres daxil edin.";
        public const string InvalidIDInput = "The department ID cannot be empty. Please enter a valid ID.";
        public const string InvalidDepartmentIDFormat = "The department ID must consist of positive integers only.";
        public const string EmployeeIDValidationError = "The employee ID must be a positive number, and negative signs or special characters cannot be included.";
        public const string InvalidEmployeeIdInput = "Please enter a valid Employee Id.";
        public const string EmployeeNotFoundbyID = $"No employee found with this ID.";
        public const string EmployeeLookupError = $"No employee found with this ID. Please enter a valid department ID.";
        public const string InvalidInput = "Invalid Department Id. Please enter a valid ID.";
        public const string DepartmentNameValidationError = "The department name must consist of letters only, and cannot include numbers, negative numbers, or special characters.";
        public const string DepartmentNotFoundbyName = $"No department found with this name.";
        public const string NoEmployeesFound = $"There are no employees.";
        public const string NotFoundEmployee = "Employee not found";
        public const string Invalid = "Invalid input. Please enter a valid number for the ID.";
        public const string Cannot = "Age cannot be empty. Please enter a valid age.";
        public const string Between = "Age must be a positive number between 18 and 67.";
        public const string Employee = $"No employee found with this age. Please enter a valid age.";
        public const string NoEmployeesAvailable = "There are no employees in the system.";
        public const string EmployeeNotFoundbyNameorSurname = $"No employee found with the given first name or surname. Please try again:";
        public const string nvalidEmployeeIDInput = "Please enter a valid employee ID.";
        public const string EmployeeNameValidationError = "The employee's name cannot contain numbers or special characters (e.g., @, #, &, ...).";
        public const string EmployeeSurnameValidationError = "The employee's surname cannot contain numbers or special characters (e.g., @, #, &, ...).";
        public const string DepartmentNotFoundbyID = $"No department found with this department ID.";
        public const string Invalidd = "Age is invalid. Please enter a valid age.";

    }
}
