namespace CarShop.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Car
    {
        public Car()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Issues = new HashSet<Issue>();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(20)]
        public string Model { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public string PictureUrl { get; set; }

        [Required]
        [RegularExpression("[A-Z]{2}[0-9]{4}[A-Z]{2}$")]
        public string PlateNumber { get; set; }

        [Required]
        [ForeignKey("FK_Owner_Cars")]
        public string OwnerId { get; set; }
      
        public User Owner { get; set; }

        public IEnumerable<Issue> Issues { get; set; }
    }
}
