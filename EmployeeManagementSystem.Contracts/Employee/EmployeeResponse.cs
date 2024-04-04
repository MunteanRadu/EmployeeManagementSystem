namespace EmployeeManagementSystem.Contracts.Employee;

public record EmployeeResponse(
    Guid ID,
    string FirstName,
    string LastName,
    int age,
    DateTime StartDateTime,
    DateTime DateOfBirth,
    DateTime LastModifiedDateTime,
    List<string> SkillSet);