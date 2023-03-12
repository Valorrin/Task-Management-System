using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Data;
using TaskManagementSystem.Data.Models;
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


            var employeeData = new Employee
            {
                FirstName = employee.FirstName,
                LastName= employee.LastName,
                BirthDate = employee.BirthDate,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                Salary = employee.Salary,
            };

            this.data.Employees.Add(employeeData);

            this.data.SaveChanges();

            return RedirectToAction("All", "Employees");
        }

        public IActionResult All([FromQuery] AllEmployeesQueryModel query)
        {
            var EmployeeQuery = this.data.Employees.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.SearchTerm))
            {
                EmployeeQuery = EmployeeQuery.Where(f =>
                    f.FirstName.ToLower().Contains(query.SearchTerm.ToLower()));
            }

            EmployeeQuery = query.Sorting switch
            {
                EmployeeSorting.FirstName => EmployeeQuery.OrderBy(f => f.FirstName),
                EmployeeSorting.CompletedTasks => EmployeeQuery.OrderByDescending(f => f.CompletedTasksCount),
                EmployeeSorting.BirthDate => EmployeeQuery.OrderBy(f=>f.BirthDate),
                EmployeeSorting.Salary => EmployeeQuery.OrderByDescending(f => f.Salary)
            };

            var totalEmployees = EmployeeQuery.Count();

            var employees = EmployeeQuery
                .Skip((query.CurrentPage - 1) * AllEmployeesQueryModel.EmployeesPerPage)
                .Take(AllEmployeesQueryModel.EmployeesPerPage)
                .Select(e => new EmployeeListingViewModel
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Email = e.Email,
                    PhoneNumber = e.PhoneNumber,
                    BirthDate = e.BirthDate,
                    Salary = e.Salary,
                    CompletedTasksCount = e.CompletedTasksCount
                })
                .ToList();

            var employeeNames = this.data
                .Employees
                .Select(f => f.FirstName)
                .Distinct()
                .OrderBy(fn => fn)
                .ToList();

            query.TotalEmployees = totalEmployees;
            query.Names = employeeNames;
            query.Employees = employees;

            return View(query);


        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var employee = this.data
                .Employees
                .Where(e => e.Id == id).First();

            var employeeModel = new AddEmployeeFormModel
            {
                Id = employee.Id,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                BirthDate = employee.BirthDate,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                Salary = employee.Salary,               
            };

            return View(employeeModel);
        }

        [HttpPost]
        public IActionResult Edit(AddEmployeeFormModel employee)
        {
            var employeeData = this.data
                .Employees
                .Where(e => e.Id == employee.Id)
                .FirstOrDefault();

            if (employeeData != null)
            {
                employeeData.FirstName = employee.FirstName;
                employeeData.LastName = employee.LastName;
                employeeData.BirthDate = employee.BirthDate;
                employeeData.Email = employee.Email;
                employeeData.Salary = employee.Salary;
                employeeData.PhoneNumber = employee.PhoneNumber;

                this.data.SaveChanges();

                return RedirectToAction("All", "Employees");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var employeeData = this.data
                .Employees
                .Where(e => e.Id == id)
                .FirstOrDefault();

            if (employeeData != null)
            {
                this.data.Employees.Remove(employeeData);
                this.data.SaveChanges();

            }

            return RedirectToAction("All");
        }
    }
}
