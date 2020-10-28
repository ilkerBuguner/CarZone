namespace CarZone.Server.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using CarZone.Server.Data.Common;

    public class Image : BaseDeletableModel<string>
    {
        public Image()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string Url { get; set; }

        [Required]
        [ForeignKey(nameof(Models.Advertisement))]
        public string AdvertisementId { get; set; }

        public virtual Advertisement Advertisement { get; set; }
    }
}
