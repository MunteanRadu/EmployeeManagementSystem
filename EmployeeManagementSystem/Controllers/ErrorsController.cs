using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagementSystem.Controllers;

public class ErrorsController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        return Problem();
    }
}