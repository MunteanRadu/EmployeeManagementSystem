using EmployeeManagementSystem.Contracts.Employee;
using EmployeeManagementSystem.Models;
using ErrorOr;

namespace EmployeeManagementSystem.Services.Employees;

public interface IEmployeeService
{
    ErrorOr<Created> CreateEmployee(Employee employee);
    ErrorOr<Employee> GetEmployee(Guid id);
    ErrorOr<UpsertedEmployee> UpsertEmployee(Employee employee);
    ErrorOr<Deleted> DeleteEmployee(Guid id);
}