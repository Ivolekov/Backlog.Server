namespace Backlog.Server.Data.Models
{
    using Microsoft.AspNetCore.Identity;
    using ServiceManager.Models;
    using System.Collections.Generic;

    public class User : IdentityUser
    {
        public IEnumerable<ServiceProtocol> ServiceProtocols { get; } = new HashSet<ServiceProtocol>();
        public IEnumerable<CompanyProfile> CompaniesProfile { get; } = new HashSet<CompanyProfile>();
    }
}
