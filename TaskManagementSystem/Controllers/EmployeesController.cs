using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Models.Employees;

namespace TaskManagementSystem.Controllers
{
    public class EmployeesController : Controller
    {
        public IActionResult Add() => View();

        [HttpPost]
        public IActionResult Add(AddEmployeeFormModel employee)
        {
            return View();
        }
    }
}
