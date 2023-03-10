using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Data;
using TaskManagementSystem.Models.Employees;
using TaskManagementSystem.Models.Tasks;

namespace TaskManagementSystem.Controllers
{
    public class TasksController : Controller
    {
        private readonly TMSystemDBContext data;

        public TasksController(TMSystemDBContext data)
            => this.data = data;


        public IActionResult Create() => View( new CreateTaskFormModel
            {
                 Assignees = this.GetEmployees()
            });

        [HttpPost]
        public IActionResult Create(CreateTaskFormModel task)
        {
            return View();
        }

        private IEnumerable<TaskAsigneeViewModel> GetEmployees()
            => this.data
            .Employees
            .Select(e => new TaskAsigneeViewModel
            {
                Id = e.Id,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Email = e.Email,
                PhoneNumber = e.PhoneNumber,
                Salary = e.Salary,
                BirthDate= e.BirthDate,
            })
            .ToList();
    }
}
