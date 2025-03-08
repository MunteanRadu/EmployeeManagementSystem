using EmployeeManagementSystem.Contracts.Employee;
using EmployeeManagementSystem.Models;
using ErrorOr;

namespace EmployeeManagementSystem.Services.Employees;

public interface IEmployeeService
{
    Task<ErrorOr<List<Employee>>> GetEmployees();
    Task<ErrorOr<Created>> CreateEmployee(Employee employee);
    Task<ErrorOr<Employee>> GetEmployee(string id);
    Task<ErrorOr<UpsertedEmployee>> UpsertEmployee(Employee employee);
    Task<ErrorOr<Deleted>> DeleteEmployee(string id);
}