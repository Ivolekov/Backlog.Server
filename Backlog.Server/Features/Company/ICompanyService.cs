namespace Backlog.Server.Features.Company
{
    using ServiceManager.Models;
    using System.Threading.Tasks;
    public interface ICompanyService
    {
        Task<CompanyProfile> GetCompanyProfile(string userId);
        Task UpdateCompanyProfile(CompanyProfile companyProfile);
        Task CreateCompanyProfile(CompanyProfile profile);
    }
}
