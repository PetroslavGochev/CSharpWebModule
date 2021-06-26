namespace BattleCards.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Common.GlobalConstants;

    public class User
    {    
        public User()
        {
            this.Id = Guid.NewGuid().ToString();
            this.UserCards = new HashSet<UserCard>();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        [MinLength(MinLengthUsername)]
        [MaxLength(MaxLengthUsername)]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public IEnumerable<UserCard> UserCards { get; set; }
    }
}
