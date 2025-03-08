namespace EmployeeManagementSystem.Contracts.Employee;

public record EmployeeResponse(
    string ID,
    string FirstName,
    string LastName,
    int age,
    DateTime StartDateTime,
    DateTime DateOfBirth,
    DateTime LastModifiedDateTime,
    List<string> SkillSet
);