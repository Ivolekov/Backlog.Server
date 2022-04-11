namespace Backlog.Server.Features.Company
{
    using Backlog.Server.Data;
    using Microsoft.EntityFrameworkCore;
    using ServiceManager.Models;
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    public class CompanyService : ICompanyService
    {
        private readonly BacklogDbContext context;
        public CompanyService(BacklogDbContext context) => this.context = context;

        public async Task<CompanyProfile> GetCompanyProfile(string userId)
        {
            try
            {
                return await this.context.CompanyProfile.Where(c => c.UserId == userId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task UpdateCompanyProfile(CompanyProfile companyProfile)
        {
            try
            {
                companyProfile.C_DateTime = DateTime.Now;
                companyProfile.C_User = "System";
                this.context.CompanyProfile.Update(companyProfile);
                await this.context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task CreateCompanyProfile(CompanyProfile profile)
        {
            try
            {
                profile.Id = 0;
                this.context.CompanyProfile.Add(profile);
                await this.context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
