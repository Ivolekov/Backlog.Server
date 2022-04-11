namespace Backlog.Server.Features.Company.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public class CompanyProfileReqestModel
    {
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
    }
}
