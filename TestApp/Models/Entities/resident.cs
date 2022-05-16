using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TestApp.Models.Entities
{
    public class resident
    {   
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid resident_id { get; set; }
        
        public string first_name { get; set; }
        
        public string second_name { get; set; }
        
        public string last_name { get; set; }
        
        public int act_status { get; set; }
        
        public int is_owner { get; set; }
        
        
        public Guid personal_account_id { get; set; }
        
        [ForeignKey("personal_account_id")]
        public personal_account PersonalAccount { get; set; }
    }
}