using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeDoSoftware.Domain.Enums;

namespace WeDoSoftware.Application.DTOs.TrainingDTO
{
    public class TrainingDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public ExerciseType ExerciseType { get; set; }
        public int DurationInMinutes { get; set; }
        public int Calories { get; set; }
        [Range(1, 10)]
        public int Intensity { get; set; }
        [Range(1, 10)]
        public int Fatigue { get; set; }
        public string? Notes { get; set; }
        public DateTime DateTime { get; set; }
    }
}
