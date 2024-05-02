using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TesteSPA.API.Model;

namespace TesteSPA.API.DTO
{
    public class TaskDTO
    {
        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public DateTime dateTime { get; set; }

        [Required]
        public string time { get; set; }

        public TaskModel toTaskModel() {
            return new TaskModel
            {
                Title = this.Title,
                Description = this.Description,
                dateTime = this.dateTime,
                time = TimeOnly.Parse(this.time)
            };
        }
    }
}