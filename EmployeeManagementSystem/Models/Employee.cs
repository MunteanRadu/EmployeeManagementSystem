using EmployeeManagementSystem.Contracts.Employee;
using EmployeeManagementSystem.ServiceErrors;
using ErrorOr;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EmployeeManagementSystem.Models;

public class Employee
{
    public const int MinNameLength = 2;
    public const int MaxNameLength = 50;

    public const int MinAge = 18;
    public const int MaxAge = 65;

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }
    
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime DateOfBirth { get; set; }
    public DateTime LastModifiedDateTime { get; set; }
    public List<string> SkillSet { get; set; }

    private Employee(
        string id, 
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
        SkillSet = skillset;
    }

    public static ErrorOr<Employee> Create(
        string firstName, 
        string lastName,
        int age,
        DateTime startDateTime,
        DateTime dateOfBirth,
        List<string> skillset,
        string? id = null)
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
            id ?? ObjectId.GenerateNewId().ToString(),
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
            request.Age,
            request.StartDateTime,
            request.DateOfBirth,
            request.SkillSet);
    }

    public static ErrorOr<Employee> From(string id, UpsertEmployeeRequest request)
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