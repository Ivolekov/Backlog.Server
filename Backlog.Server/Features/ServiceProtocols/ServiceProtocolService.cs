namespace Backlog.Server.Features.ServiceProtocols
{
    using Backlog.Server.Data;
    using Backlog.Server.Data.Models;
    using Backlog.Server.Features.ServiceProtocols.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    public class ServiceProtocolService : IServiceProtocolService
    {
        private readonly BacklogDbContext context;
        public ServiceProtocolService(BacklogDbContext context) => this.context = context;

        public async Task SetServiceProtocolStatus(int id, int statusId)
        {
            try
            {
                context.ServiceProtocols.Find(id).ServiceProtocolStatusId = statusId;
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<IServiceProtocol> CreateServiceProtocol(IServiceProtocol serviceProtocol)
        {
            try
            {
                serviceProtocol.C_DateTime = DateTime.Now;
                serviceProtocol.DateOfIssue = DateTime.Now;
                context.ServiceProtocols.Add(serviceProtocol);
                await context.SaveChangesAsync();
                return serviceProtocol;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteServiceProtocol(IServiceProtocol serviceProtocol)
        {
            try
            {
                // await CreateServiceProtocolArhive(serviceProtocol);
                context.ServiceProtocols.Remove(serviceProtocol);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IServiceProtocol> GetServiceProtocolById(int id)
        {
            try
            {
                return await context.ServiceProtocols.Where(x => x.Id == id && x.isDeleted == 0).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<IServiceProtocol>> GetServiceProtocolsList(string userId)
        {
            try
            {
                return await context.ServiceProtocols.Where(p=>p.isDeleted == 0 && p.UserId == userId).OrderByDescending(p => p.DateOfIssue).ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<IServiceProtocol>> Search(string input, string userId)
        {
            try
            {
                return await context.ServiceProtocols.Where(x => (x.ClientPhone.Contains(input) || x.BrandModel.Contains(input)) && x.isDeleted == 0 && x.UserId == userId).OrderByDescending(x => x.C_DateTime).ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateServiceProtocol(IServiceProtocol serviceProtocol)
        {
            try
            {
                serviceProtocol.C_DateTime = DateTime.Now;
                context.ServiceProtocols.Update(serviceProtocol);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task SetServiceProtocolDeleteFlag(int protocolId, int isdeleted)
        {
            var serviceProtocol = await GetServiceProtocolById(protocolId);
            serviceProtocol.isDeleted = isdeleted;
            context.ServiceProtocols.Update(serviceProtocol);
            await context.SaveChangesAsync();
        }
    }
}
