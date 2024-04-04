using ErrorOr;

namespace EmployeeManagementSystem.ServiceErrors;

public static class Errors
{
    public static class Employee
    {
        public static Error InvalidName => Error.Validation(
            code: "Employee.InvalidName",
            description: $"Employee first and last name must be at least {Models.Employee.MinNameLength}" +
             $" characters long and at most {Models.Employee.MaxNameLength} characters long.");

        public static Error InvalidAge => Error.Validation(
            code: "Employee.InvalidAge",
            description: $"Employee age must be at least {Models.Employee.MinNameLength}" +
             $" and at most {Models.Employee.MaxNameLength}.");

        public static Error NotFound => Error.NotFound(
            code: "Employee.NotFound",
            description: "Employee not found");
    }
}