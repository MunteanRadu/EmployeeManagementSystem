using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Controllers;

public class ErrorsController : ControllerBase
{
    [HttpGet]
    public IActionResult Error()
    {
        return Problem();
    }
}