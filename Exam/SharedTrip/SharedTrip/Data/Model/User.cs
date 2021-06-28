﻿namespace SharedTrip.Data.Model
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
            this.UserTrip = new HashSet<UserTrip>();
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

        public IEnumerable<UserTrip> UserTrip { get; set; }
    }
}
