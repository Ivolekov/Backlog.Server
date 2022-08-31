namespace Backlog.Server.Features.ServiceProtocols.Models
{
    using Backlog.Server.Data.Models;
    using System;
    using System.ComponentModel.DataAnnotations;
    using static Backlog.Server.Data.Validation.ServiceProtocol;

    public class ServiceProtocol : IServiceProtocol
    {

        [Key]
        public int Id { get; set; }

        public string ClientEmail { get; set; }

        [Required]
        public string ClientPhone { get; set; }

        public DateTime? C_DateTime { get; set; }

        public int ServiceProtocolStatusId { get; set; }

        [Required]
        [MaxLength(MaxBrandModelLength)]

        public string BrandModel { get; set; }

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }

        public string Comment { get; set; }

        public bool SimTray { get; set; }

        public bool Charger { get; set; }

        public bool Bag { get; set; }

        public string Other { get; set; }

        public string Pin { get; set; }

        public string UnlockPass { get; set; }

        public string LockPattern { get; set; }

        public decimal? FinalPrice { get; set; }

        public string ServiceAction { get; set; }

        public string FaultInformation { get; set; }

        public DateTime? DateOfIssue { get; set; }

        public string ServicePartsJson { get; set; }

        public int isDeleted { get; set; }
    }
}
