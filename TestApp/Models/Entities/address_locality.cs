using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestApp.Models.Entities
{
    public class address_locality
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string locality_id { get; set; }
        
        public string address_id { get; set; }
        
        public int act_status { get; set; }
        
        public int level { get; set; }
        
        public int curr_status { get; set; }
            
        public DateTime? end_date { get; set; }
        
        [MaxLength(120)]    
        public string address_name { get; set; }    
        
        public string next_id { get; set; }
        
        public string parent_id { get; set; }
        
        public string prev_id { get; set; }
        
        public string short_type_name { get; set; }

        public DateTime? start_date { get; set; }
        
        public string add_user { get; set; }
    }
}