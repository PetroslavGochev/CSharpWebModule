using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SulsApp.Data.Model
{
    public class User
    {
        public User()
        {
            this.Submissions = new List<Submission>();
        }
        public string Id { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(20)]
        public string Username { get; set; }
            
        [Required]
        public string Email { get; set; }


        [Required]
        [MinLength(6)]
        [MaxLength(20)]
        public string Password { get; set; }

        public virtual ICollection<Submission> Submissions { get; set; }
    }
}
