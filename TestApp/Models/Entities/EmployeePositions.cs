using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;

namespace TestApp.Models.Entities
{
    public class EmployeePositions
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid employee_position_id { get; set; }
        
        [Required]
        public string employee_id { get; set; }
        public Guid position_id { get; set; }
        
        [ForeignKey("employee_id")]
        public Employee Employee { get; set; }
        
        [ForeignKey("position_id")]
        public Position Position { get; set; }
    }
}