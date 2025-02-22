using EmployeeManagementSystem.Models;
using EmployeeManagementSystem.ServiceErrors;
using EmployeeManagementSystem.Services.Employees;
using ErrorOr;
using MongoDB.Driver;

namespace EmployeeManagementSystem.Services.Employees;

public class EmployeeService : IEmployeeService
{
    private readonly IMongoCollection<Employee> _employeeCollection;

    public EmployeeService(string connectionString, string databaseName)
    {
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);
        _employeeCollection = database.GetCollection<Employee>("Employees");
    }

    public async Task<ErrorOr<Created>> CreateEmployee(Employee employee)
    {
        await _employeeCollection.InsertOneAsync(employee);
        return Result.Created;
    }

    public async Task<ErrorOr<Deleted>> DeleteEmployee(string id)
    {
        var result = await _employeeCollection.DeleteOneAsync(emp => emp.Id == id);

        if(result.DeletedCount > 0)
        {
            return Result.Deleted;
        }

        return Errors.Employee.NotFound;
    }

    public async Task<ErrorOr<Employee>> GetEmployee(string id)
    {
        var employee = await _employeeCollection.Find(emp => emp.Id == id).FirstOrDefaultAsync();

        if (employee != null)
        {
            return employee;
        }

        return Errors.Employee.NotFound;
    }

    public async Task<ErrorOr<UpsertedEmployee>> UpsertEmployee(Employee employee)
    {
        var result = await _employeeCollection.ReplaceOneAsync(
            emp => emp.Id == employee.Id,
            employee,
            new ReplaceOptions { IsUpsert = true}
        );

        var isNewlyCreated = result.UpsertedId != null;
        return new UpsertedEmployee(isNewlyCreated);
    }
}