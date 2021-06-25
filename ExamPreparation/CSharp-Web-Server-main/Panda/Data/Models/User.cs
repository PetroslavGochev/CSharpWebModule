namespace Panda.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User
    {
        public User()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Packages = new HashSet<Package>();
            this.Receipts = new HashSet<Receipt>();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(20)]
        public string Username { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(20)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public IEnumerable<Package> Packages { get; set; }

        public IEnumerable<Receipt> Receipts { get; set; }
    }
}
