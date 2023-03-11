using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using static TaskManagementSystem.Data.DataConstants;

namespace TaskManagementSystem.Data.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(NameMaxLength)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(EmailMaxLength)]
        public string Email { get; set; }

        [Required]
        [MaxLength(PhoneNumberMaxLength)]
        public string PhoneNumber { get; set; }

        public DateTime BirthDate { get; set; }

        public decimal Salary { get; set; }


        public IEnumerable<Task> Tasks { get; set; } = new List<Task>();
    }
}
