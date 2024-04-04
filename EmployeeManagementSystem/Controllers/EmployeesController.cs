using EmployeeManagementSystem.Contracts.Employee;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.ServiceErrors;
using EmployeeManagementSystem.Services.Employees;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Controllers;

public class EmployeesController : ApiController
{
    private readonly IEmployeeService _employeeService;

    public EmployeesController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpPost]
    public IActionResult CreateEmployee(CreateEmployeeRequest request)
    {
        ErrorOr<Employee> requestToEmployeeResult = Employee.From(request);

        if (requestToEmployeeResult.IsError)
        {
            return Problem(requestToEmployeeResult.Errors);
        }

        var employee = requestToEmployeeResult.Value;
        ErrorOr<Created> createEmployeeResult =_employeeService.CreateEmployee(employee);

        return createEmployeeResult.Match(
            created => CreatedAtGetEmployee(employee),
            errors => Problem(errors));
    }

    [HttpGet("{id:guid}")]
    public IActionResult GetEmployee(Guid id)
    {
        ErrorOr<Employee> getEmployeeResult = _employeeService.GetEmployee(id);

        return getEmployeeResult.Match(
            employee => Ok(MapEmployeeResponse(employee)),
            errors => Problem(errors));
    }

    [HttpPut("{id:guid}")]
    public IActionResult UpsertEmployee(Guid id, UpsertEmployeeRequest request)
    {
        ErrorOr<Employee> requestToEmployeeResult = Employee.From(id, request);

        if (requestToEmployeeResult.IsError)
        {
            return Problem(requestToEmployeeResult.Errors);
        }

        var employee = requestToEmployeeResult.Value;
        ErrorOr<UpsertedEmployee> upsertEmployeeResult =_employeeService.UpsertEmployee(employee);


        return upsertEmployeeResult.Match(
            upserted => upserted.IsNewlyCreated ? CreatedAtGetEmployee(employee) : NoContent(),
            errors => Problem(errors));
    }

    [HttpDelete("{id:guid}")]
    public IActionResult DeleteEmployee(Guid id)
    {
        ErrorOr<Deleted> deleteEmployeeResult = _employeeService.DeleteEmployee(id);

        return deleteEmployeeResult.Match(
            deleted => NoContent(),
            errors => Problem(errors));
    }
    
    private static EmployeeResponse MapEmployeeResponse(Employee employee){
        return new EmployeeResponse(
            employee.Id,
            employee.FirstName,
            employee.LastName,
            employee.Age,
            employee.StartDateTime,
            employee.DateOfBirth,
            employee.LastModifiedDateTime,
            employee.Skillset);
    }

    private CreatedAtActionResult CreatedAtGetEmployee(Employee employee)
    {
        return CreatedAtAction(
            actionName: nameof(GetEmployee),
            routeValues: new { id = employee.Id },
            value: MapEmployeeResponse(employee));
    }
}

