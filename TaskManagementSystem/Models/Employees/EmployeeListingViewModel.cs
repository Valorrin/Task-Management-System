using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.Models.Employees
{
    public class EmployeeListingViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        [BindProperty, DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        public decimal Salary { get; set; }

       // public IEnumerable<Data.Models.Task> Tasks { get; set; }
    }
}
