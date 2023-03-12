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

            return RedirectToAction("All", "Tasks");

        }

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


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var task = this.data
                .Tasks
                .Where(e => e.Id == id).First();


            var taskModel = new CreateTaskFormModel
            {
                Id = task.Id,
                Title = task.Title,
                Description =task.Description,
                DueDate = task.DueDate,
                AssigneeId = task.AssigneeId,
                Assignees = this.GetEmployees()

        };

            return View(taskModel);
        }

        [HttpPost]
        public IActionResult Edit(CreateTaskFormModel task)
        {
            var taskData = this.data
                .Tasks
                .Where(e => e.Id == task.Id)
                .FirstOrDefault();

            if (taskData != null)
            {
                taskData.Title = task.Title;
                taskData.Description = task.Description;
                taskData.DueDate = task.DueDate;
                taskData.Assignee = (Employee)this.data
                .Employees
                .Where(x => x.Id == task.AssigneeId).First();

                this.data.SaveChanges();

                return RedirectToAction("All");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var taskData = this.data
                .Tasks
                .Where(e => e.Id == id)
                .FirstOrDefault();

            if (taskData != null)
            {
                this.data.Tasks.Remove(taskData);
                this.data.SaveChanges();

            }

            return RedirectToAction("All");
        }

        [HttpGet]
        public IActionResult CompleteTask(int id)
        {
            var taskData = this.data
               .Tasks
               .Where(e => e.Id == id)
               .FirstOrDefault();

            var list = this.data
                    .Employees
                    .Where(e => e.Id == taskData.AssigneeId)
                    .Select(e => e.CompletedTasks)
                    .ToList();

            if (taskData != null)
            {
                var newEmployeeData = this.data
                    .Employees
                    .Where(e=> e.Id == taskData.AssigneeId)
                    .Select(x => new AddEmployeeFormModel
                    {
                        
                        FirstName= x.FirstName,
                        BirthDate = x.BirthDate,
                        Email= x.Email,
                        Id = x.Id,
                        LastName = x.LastName,
                        PhoneNumber = x.PhoneNumber,
                        Salary = x.Salary,
                        CompletedTasks = x.CompletedTasks

                    });

                
                var oldEmployee = this.data
                    .Employees
                    .Where(e => e.Id == id).FirstOrDefault();


                this.data.Tasks.Remove(taskData);

                this.data.SaveChanges();
            }

            return RedirectToAction("All");
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
               BirthDate = e.BirthDate,
           })
           .ToList();
    }


}
