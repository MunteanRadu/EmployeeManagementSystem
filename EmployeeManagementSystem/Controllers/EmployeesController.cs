using EmployeeManagementSystem.Contracts.Employee;
using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.ServiceErrors;
using EmployeeManagementSystem.Services.Employees;
using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using MongoDB.Bson;

namespace EmployeeManagementSystem.Controllers;

public class EmployeesController : ApiController
{
    private readonly IEmployeeService _employeeService;

    public EmployeesController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpGet]
    public async Task<IActionResult> GetEmployees()
    {
        ErrorOr<List<Employee>> getEmployeesResult = await _employeeService.GetEmployees();

        return getEmployeesResult.Match(
            employees => Ok(employees.Select(employee => MapEmployeeResponse(employee))),
            errors => Problem(errors)
        );
    }

    [HttpPost]
    public async Task<IActionResult> CreateEmployee(CreateEmployeeRequest request)
    {
        var employee = Employee.From(request).Value;
        ErrorOr<Created> createEmployeeResult = await _employeeService.CreateEmployee(employee);
    
        return createEmployeeResult.Match(
            created => CreatedAtGetEmployee(employee),
            errors => Problem(errors)
        );
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEmployee(string id)
    {
        if(!ObjectId.TryParse(id, out var objectId))
        {
            return BadRequest("Invalid ObjectId format.");
        }

        ErrorOr<Employee> getEmployeeResult = await _employeeService.GetEmployee(id);

        return getEmployeeResult.Match(
            employee => Ok(MapEmployeeResponse(employee)),
            errors => Problem(errors)
        );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpsertEmployee(string id, UpsertEmployeeRequest request)
    {
        if(!ObjectId.TryParse(id, out var objectId))
        {
            return BadRequest("Invalid ObjectId format.");
        }

        ErrorOr<Employee> requestToEmployeeResult = Employee.From(objectId.ToString(), request);

        if(requestToEmployeeResult.IsError)
        {
            return Problem(requestToEmployeeResult.Errors);
        }

        var employee = requestToEmployeeResult.Value;
        ErrorOr<UpsertedEmployee> upsertEmployeeResult = await _employeeService.UpsertEmployee(employee);

        return upsertEmployeeResult.Match(
            upserted => upserted.IsNewlyCreated ? CreatedAtGetEmployee(employee) : NoContent(),
            errors => Problem(errors) 
        );
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployee(string id)
    {
        if(!ObjectId.TryParse(id, out var objectId))
        {
            return BadRequest("Invalid ObjectId format.");
        }

        ErrorOr<Deleted> deleteEmployeeResult = await _employeeService.DeleteEmployee(id);

        return deleteEmployeeResult.Match(
            deleted => NoContent(),
            errors => Problem(errors)
        );
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
            employee.SkillSet);
    }

    private CreatedAtActionResult CreatedAtGetEmployee(Employee employee)
    {
        return CreatedAtAction(
            actionName: nameof(GetEmployee),
            routeValues: new { id = employee.Id },
            value: MapEmployeeResponse(employee));
    }
}

