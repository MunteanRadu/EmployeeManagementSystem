using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.ServiceErrors;
using EmployeeManagementSystem.Services.Employees;
using ErrorOr;

namespace EmployeeManagementSystem.Services.Employees;

public class EmployeeService : IEmployeeService
{
    private static readonly Dictionary<Guid, Employee> _employees = new();
    
    public ErrorOr<Created> CreateEmployee(Employee employee)
    {
        _employees.Add(employee.Id, employee);

        return Result.Created;
    }

    public ErrorOr<Deleted> DeleteEmployee(Guid id)
    {
        _employees.Remove(id);

        return Result.Deleted;
    }

    public ErrorOr<Employee> GetEmployee(Guid id)
    {
        if(_employees.TryGetValue(id, out var employee))
        {
            return employee;
        }

        return Errors.Employee.NotFound;
    }

    public ErrorOr<UpsertedEmployee> UpsertEmployee(Employee employee)
    {
        var isNewlyCreated = !_employees.ContainsKey(employee.Id);
        _employees[employee.Id] = employee;

        return new UpsertedEmployee(isNewlyCreated);
    }
}