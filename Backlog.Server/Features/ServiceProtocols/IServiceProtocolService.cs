namespace Backlog.Server.Features.ServiceProtocols
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Backlog.Server.Data.Models;
    using Backlog.Server.Features.ServiceProtocols.Models;

    public interface IServiceProtocolService
    {
        Task<IServiceProtocol> CreateServiceProtocol(IServiceProtocol serviceProtocol);
        Task<IEnumerable<IServiceProtocol>> GetServiceProtocolsList(string userId);
        Task<IServiceProtocol> GetServiceProtocolById(int id);
        Task UpdateServiceProtocol(IServiceProtocol serviceProtocol);
        Task DeleteServiceProtocol(IServiceProtocol serviceProtocol);
        Task<IEnumerable<IServiceProtocol>> Search(string input, string userId);
        Task SetServiceProtocolStatus(int id, int statusId);
        Task SetServiceProtocolDeleteFlag(int protocolId, int isdeleted);
    }
}
