using EmployeeManagementSystem.Contracts.Employee;
using EmployeeManagementSystem.ServiceErrors;
using ErrorOr;

namespace EmployeeManagementSystem.Models;

public class Employee
{
    public const int MinNameLength = 2;
    public const int MaxNameLength = 50;

    public const int MinAge = 18;
    public const int MaxAge = 65;

    public Guid Id { get; }
    public string FirstName { get; }
    public string LastName { get; }
    public int Age { get; }
    public DateTime StartDateTime { get; }
    public DateTime DateOfBirth { get; }
    public DateTime LastModifiedDateTime { get; }
    public List<string> Skillset { get; }

    private Employee(
        Guid id, 
        string firstName, 
        string lastName,
        int age,
        DateTime startDateTime,
        DateTime dateOfBirth,
        DateTime lastModifiedDateTime, 
        List<string> skillset)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Age = age;
        StartDateTime = startDateTime;
        DateOfBirth = dateOfBirth;
        LastModifiedDateTime = lastModifiedDateTime;
        Skillset = skillset;
    }

    public static ErrorOr<Employee> Create(
        string firstName, 
        string lastName,
        int age,
        DateTime startDateTime,
        DateTime dateOfBirth,
        List<string> skillset,
        Guid? id = null)
    {
        List<Error> errors = new();

        if(firstName.Length is < MinNameLength or > MaxNameLength)
        {
            errors.Add(Errors.Employee.InvalidName);
        }

        if(lastName.Length is < MinNameLength or > MaxNameLength)
        {
            errors.Add(Errors.Employee.InvalidName);
        }

        if(age is < MinAge or > MaxAge)
        {
            errors.Add(Errors.Employee.InvalidAge);
        }

        if(errors.Count > 0)
        {
            return errors;
        }

        return new Employee(
            id ?? Guid.NewGuid(),
            firstName,
            lastName,
            age,
            startDateTime,
            dateOfBirth,
            DateTime.UtcNow,
            skillset);
    }

    public static ErrorOr<Employee> From(CreateEmployeeRequest request)
    {
        return Create(
            request.FirstName,
            request.LastName,
            request.age,
            request.StartDateTime,
            request.DateOfBirth,
            request.SkillSet);
    }

    public static ErrorOr<Employee> From(Guid id, UpsertEmployeeRequest request)
    {
        return Create(
            request.FirstName,
            request.LastName,
            request.age,
            request.StartDateTime,
            request.DateOfBirth,
            request.SkillSet,
            id);
    }
}