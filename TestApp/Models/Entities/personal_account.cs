using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestApp.Models.Entities
{
    public class personal_account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid personal_account_id { get; set; }
        
        [Required]
        public string lic_sch { get; set; }
        
        
        public apartment Apartment { get; set; }
            
        public List<resident> Residents { get; set; }
    }
}