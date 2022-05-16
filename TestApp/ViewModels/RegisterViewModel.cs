using System;
using System.ComponentModel.DataAnnotations;
using TestApp.Models;
using TestApp.Models.Entities;
using System.Collections.Generic;

namespace TestApp.ViewModels
{
    public class RegisterViewModel
    {
        [Display(Name = "Логин")]
        [Required(ErrorMessage = "- Введите логин")]
        public string Login { get; set; }
        
        [Display(Name = "Роли: ")]
        public List<string> RoleNames { get; set; }
        
        [Display(Name = "Должности: ")]
        public List<string> PositionNames { get; set; }
        
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        [Required(ErrorMessage = "- Введите пароль")]
        public string Password { get; set; }
        
        public List<string> RoleNameList { get; set; }
        
        public List<string> PositionNameList { get; set; }  

    
        public RegisterViewModel()
        {
            RoleNameList = new List<string>();
            PositionNameList = new List<string>();
        }
    }
}