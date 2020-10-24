namespace CarZone.Server.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using CarZone.Server.Data.Common;

    using static CarZone.Server.Data.Common.Constants.Advertisement;

    public class Advertisement : BaseDeletableModel<string>
    {
        public Advertisement()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        [Required]
        [MaxLength(PhoneNumberMaxLength)]
        public int PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public int Views { get; set; }

        [Required]
        public string CarId { get; set; }

        public Car Car { get; set; }

        [ForeignKey(nameof(Models.User))]
        [Required]
        public string AuthorId { get; set; }

        public virtual User Author { get; set; }

        public string ImageURLs { get; set; }
    }
}
