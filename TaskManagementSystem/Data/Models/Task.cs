using System.ComponentModel.DataAnnotations;
using static TaskManagementSystem.Data.DataConstants;

namespace TaskManagementSystem.Data.Models
{
    public class Task
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        public DateTime DueDate { get; set; }

        public int AssigneeId { get; set; }
        public Employee? Assignee { get; set; }

    }
}
