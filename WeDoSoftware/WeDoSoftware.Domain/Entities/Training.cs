using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeDoSoftware.Domain.Enums;

namespace WeDoSoftware.Domain.Entities
{
    public class Training
    {
        public int Id { get; set; }               
        public int UserId { get; set; }           
        public DateTime DateTime { get; set; }
        public ExerciseType ExerciseType { get; set; }
        public int DurationInMinutes { get; set; }
        public int Calories { get; set; }
        public int Intensity { get; set; }        
        public int Fatigue { get; set; }          
        public string? Notes { get; set; }

        public User? User { get; set; }           
    }
}
