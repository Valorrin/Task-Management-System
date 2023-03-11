using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TaskManagementSystem.Data.Models;

namespace TaskManagementSystem.Models.Tasks
{
    public class TaskListingViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        [BindProperty, DataType(DataType.Date)]
        public DateTime DueDate { get; set; }

        public string Assignee { get; set; }
    }
}
