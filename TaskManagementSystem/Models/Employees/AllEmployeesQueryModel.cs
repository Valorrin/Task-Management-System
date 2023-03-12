using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.Models.Employees
{
    public class AllEmployeesQueryModel
    {
        public const int EmployeesPerPage = 2;

        public string Name { get; set; }
        public IEnumerable<string> Names { get; set; }

        public string SearchTerm { get; set; }

        public EmployeeSorting Sorting { get; set; }

        public int CurrentPage { get; set; } = 1;

        public int TotalEmployees { get; set; }

        public IEnumerable<EmployeeListingViewModel> Employees { get; set; }
    }
}
