using TaskManagementSystem.Data.Models;
using TaskManagementSystem.Models.Employees;

namespace TaskManagementSystem.Models.Tasks
{
    public class TaskAsigneeViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime BirthDate { get; set; }

        public decimal Salary { get; set; }

    }
}
