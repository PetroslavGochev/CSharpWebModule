namespace SulsProblemDescription.Data.Model
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Submission
    {
        public Submission()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
        }

        [Key]
        public string Id { get; set; }

        [Required]
        [MinLength(30)]
        [MaxLength(800)]
        public string Code { get; set; }

        [Required]
        [Range(0,300)]
        public int AchievedResult { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [ForeignKey("FK_Problem_Submission")]
        public string ProblemId { get; set; }

        public Problem Problem { get; set; }

        [ForeignKey("FK_User_Submission")]
        public string UserId { get; set; }

        public User User { get; set; }
    }
}
