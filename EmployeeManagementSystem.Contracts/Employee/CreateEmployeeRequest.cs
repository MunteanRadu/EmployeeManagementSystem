namespace EmployeeManagementSystem.Contracts.Employee;

public record CreateEmployeeRequest(
    string FirstName,
    string LastName,
    int age,
    DateTime StartDateTime,
    DateTime DateOfBirth,
    List<string> SkillSet);