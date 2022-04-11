namespace Backlog.Server.Features.ServiceProtocols.Models
{
    using System;
    public class ServiceProtocolListResponseModel
    {
        public int Id { get; set; }

        public string ClientPhone { get; set; }

        public DateTime? C_DateTime { get; set; }

        public int ServiceProtocolStatusId { get; set; }

        public string BrandModel { get; set; }

        public decimal? FinalPrice { get; set; }

        public DateTime? DateOfIssue { get; set; }
    }
}
