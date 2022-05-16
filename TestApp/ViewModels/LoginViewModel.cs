using System;
using System.ComponentModel.DataAnnotations;

namespace TestApp.ViewModels
{
    public class LoginViewModel
    {
        public string ReturnUrl { get; set; }
        [Display(Name = "Логин")]
        [Required]
        public string EmployeeName { get; set; }
        [Display(Name = "Пароль")]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Запонмить меня")]
        public bool RememberMe { get; set; }
    }
}