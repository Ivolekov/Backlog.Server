namespace Backlog.Server.Features.Company.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class CompanyProfileResponseModel
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string CompanyPhone { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanyWebAddress { get; set; }
        public string Bulstat { get; set; }
        public string CompanyOwner { get; set; }
        public DateTime C_DateTime { get; set; }
        public string C_User { get; set; }
    }
}
