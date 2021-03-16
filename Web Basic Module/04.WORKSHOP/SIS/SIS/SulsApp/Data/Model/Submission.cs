using System;
using System.ComponentModel.DataAnnotations;

namespace SulsApp.Data.Model
{
    public class Submission
    {
        public string Id { get; set; }

        [Required]
        [MinLength(30)]
        [MaxLength(800)]
        public string Code { get; set; }

        [Required]
        [Range(0, 100)]
        public int AchievedResult { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }


        public string ProblemId { get; set; }
        public virtual Problem Problem { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}

