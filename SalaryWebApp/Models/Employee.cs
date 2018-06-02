using Newtonsoft.Json;
using SalaryContracts;
using SalaryWebApp.Validators;
using System;
using System.ComponentModel.DataAnnotations;

namespace SalaryWebApp.Models
{
    public class Employee: ITaxableEmployee
    {
        [Required(ErrorMessage = "First name is required")]
        [JsonProperty(PropertyName = "FirstName")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [JsonProperty(PropertyName = "LastName")]
        public string LastName { get; set; }

        [CustomPositiveValidator(ErrorMessage = "Salary should be positive")]
        [Required(ErrorMessage = "Anual Salary is required")]
        [JsonProperty(PropertyName = "AnualSalary")]
        public int AnualSalary { get; set; }

        [Range(0, 12, ErrorMessage = "Super rate shall be between 0 to 12")]
        [JsonProperty(PropertyName = "SuperRate")]
        public int SuperRate { get; set; }

        [Required(ErrorMessage = "Payment start is required")]
        [JsonProperty(PropertyName = "PaymentStartDate")]
        public DateTime PaymentStartDate { get; set; }
    }
}