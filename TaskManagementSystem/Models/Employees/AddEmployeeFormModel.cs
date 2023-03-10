﻿using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.Models.Employees
{
    public class AddEmployeeFormModel
    {

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime BirthDate { get; set; }

        public decimal Salary { get; set; }
    }
}