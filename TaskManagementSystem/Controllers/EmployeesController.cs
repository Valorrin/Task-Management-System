using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Data;
using TaskManagementSystem.Models.Employees;

namespace TaskManagementSystem.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly TMSystemDBContext data;

        public EmployeesController(TMSystemDBContext data)
            => this.data = data;


        public IActionResult Add() => View();

        [HttpPost]
        public IActionResult Add(AddEmployeeFormModel employee)
        {
            if (!ModelState.IsValid)
            {
                return View(employee);
            }

            return RedirectToAction("Index", "Home");
        }

        public IActionResult All()
        {
            var employees = this.data
                .Employees
                .Select(e => new EmployeeListingViewModel
                {
                    Id= e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Email= e.Email,
                    PhoneNumber= e.PhoneNumber,
                    BirthDate = e.BirthDate,
                    Salary = e.Salary,
                    Tasks = e.Tasks,
        });

            return View(employees);
        }
    }
}
