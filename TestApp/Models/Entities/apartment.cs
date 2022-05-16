using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestApp.Models.Entities
{
    public class apartment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string house_id { get; set; }
        
        public string house_guid { get; set; }

        public string house_num { get; set; }
        
        public int act_status { get; set; }
        
        public string short_type_name { get; set; }
        
        public string parent_id { get; set; }
        
        public DateTime start_date { get; set; }
        
        public DateTime? update_date { get; set; }
        
        public DateTime? end_date { get; set; }
        
        public int counter { get; set; }
        
        
        public Guid personal_account_id { get; set; }
        
        
        [ForeignKey("personal_account_id")]
        public personal_account PersonalAccount { get; set; }
    }
}