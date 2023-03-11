using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using TaskManagementSystem.Data;
using TaskManagementSystem.Data.Models;
using TaskManagementSystem.Models.Employees;
using TaskManagementSystem.Models.Tasks;

namespace TaskManagementSystem.Controllers
{
    public class TasksController : Controller
    {
        private readonly TMSystemDBContext data;

        public TasksController(TMSystemDBContext data)
            => this.data = data;


        public IActionResult Create() => View(new CreateTaskFormModel
        {
            Assignees = this.GetEmployees()
        });

        [HttpPost]
        public IActionResult Create(CreateTaskFormModel task)
        {
            if (!ModelState.IsValid)
            {
                task.Assignees = this.GetEmployees();

                return View(task);
            }

            var taskData = new Data.Models.Task
            {
                Title = task.Title,
                Description = task.Description,
                DueDate = task.DueDate,
                AssigneeId = task.AssigneeId,
                Assignee = (Employee)this.data
                .Employees
                .Where(x => x.Id == task.AssigneeId).First()
            };

         
            this.data.Tasks.Add(taskData);

            this.data.SaveChanges();

            return RedirectToAction("Index", "Home");

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

        public IActionResult All()
        {
            var tasks = this.data
                .Tasks
                .Select(e => new TaskListingViewModel
                {
                    Id = e.Id,
                    Title = e.Title,
                    Description = e.Description,
                    DueDate = e.DueDate,
                    Assignee = $"{e.Assignee.FirstName} {e.Assignee.LastName}"    

                }) ;

            return View(tasks);
        }
    }


}
