namespace BattleCards.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using static Common.GlobalConstants;

    public class Card
    {
        public Card()
        {
            this.Id = Guid.NewGuid().ToString();
            this.UserCards = new HashSet<UserCard>();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        [MinLength(MinLengthName)]
        [MaxLength(MaxLengthName)]
        public string Name { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public string Keyword { get; set; }

        [Required]
        [Range(Minimum, int.MaxValue)]
        public int Attack { get; set; }

        [Required]
        [Range(Minimum, int.MaxValue)]
        public int Health { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Descritpion { get; set; }

        public IEnumerable<UserCard> UserCards { get; set; }
    }
}
