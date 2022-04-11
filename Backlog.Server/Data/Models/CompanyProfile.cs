namespace ServiceManager.Models
{
    using Backlog.Server.Data.Models;
    using System;
    using System.ComponentModel.DataAnnotations;
    public class CompanyProfile
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        [Required]
        public string CompanyPhone { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanyWebAddress { get; set; }
        [Required]
        public string Bulstat { get; set; }
        [Required]
        public string CompanyOwner { get; set; }
        public DateTime C_DateTime { get; set; }
        public string C_User { get; set; }
        public string UserId { get; set; }
    }
}
