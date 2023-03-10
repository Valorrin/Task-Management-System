namespace TaskManagementSystem.Models.Employees
{
    public class EmployeeListingViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime BirthDate { get; set; }

        public decimal Salary { get; set; }

        public IEnumerable<Data.Models.Task> Tasks { get; set; }
    }
}
