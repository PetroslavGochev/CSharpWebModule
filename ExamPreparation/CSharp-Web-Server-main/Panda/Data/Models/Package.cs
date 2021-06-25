namespace Panda.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Package
    {
        public Package()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(20)]
        public string Description { get; set; }

        public double Weight { get; set; }

        public string ShippingAddress { get; set; }

        public string Status { get; set; }

        public DateTime EstimatedDeliveryDate  { get; set; }

        [Required]
        public string RecipientId { get; set; }

        public User Recipient { get; set; }
    }
}
