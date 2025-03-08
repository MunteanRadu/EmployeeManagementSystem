namespace EmployeeManagementSystem.Contracts.Employee;

public record UpsertEmployeeRequest(
    string FirstName,
    string LastName,
    int age,
    DateTime StartDateTime,
    DateTime DateOfBirth,
    List<string> SkillSet
);