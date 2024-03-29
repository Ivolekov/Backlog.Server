﻿namespace Backlog.Server.Features.ServiceProtocols
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Backlog.Server.Data.Models;
    using Backlog.Server.Features.ServiceProtocols.Models;

    public interface IServiceProtocolService
    {
        Task<IServiceProtocol> CreateServiceProtocol(ServiceProtocol serviceProtocol);
        Task<IEnumerable<IServiceProtocol>> GetServiceProtocolsList(string userId);
        Task<ServiceProtocol> GetServiceProtocolById(int id);
        Task UpdateServiceProtocol(ServiceProtocol serviceProtocol);
        Task DeleteServiceProtocol(ServiceProtocol serviceProtocol);
        Task<IEnumerable<IServiceProtocol>> Search(string input, string userId);
        Task SetServiceProtocolStatus(int id, int statusId);
        Task SetServiceProtocolDeleteFlag(int protocolId, int isdeleted);
    }
}
