namespace CarShop.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Issue
    {
        public Issue()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        [MinLength(5)]
        public string Description { get; set; }

        [Required]
        public bool IsFixed { get; set; }

        [Required]
        [ForeignKey("FK_Car_Issue")]
        public string CarId { get; set; }

        public Car Car { get; set; }
    }
}
