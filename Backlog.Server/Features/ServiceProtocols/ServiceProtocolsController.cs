namespace Backlog.Server.Features.ServiceProtocols
{
    using Backlog.Server.Infrastructure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using Backlog.Server.Data.Models;
    using System.Collections.Generic;
    using Backlog.Server.Features.ServiceProtocols.Models;

    [Authorize]
    public class ServiceProtocolsController : ApiController
    {
        private readonly IServiceProtocolService serviceProtocolService;
        public ServiceProtocolsController(IServiceProtocolService serviceProtocolService) 
        {
            this.serviceProtocolService = serviceProtocolService;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Create(ServiceProtocolRequestModel model)
        {

            var userId = this.User.GetId();
            var serviceProtocol = new ServiceProtocol()
            {
                Bag = model.Bag,
                BrandModel = model.BrandModel,
                Charger = model.Charger,
                ClientEmail = model.ClientEmail,
                ClientPhone = model.ClientPhone,
                Comment = model.Comment,
                C_DateTime = model.C_DateTime,
                DateOfIssue = model.DateOfIssue,
                FaultInformation = model.FaultInformation,
                FinalPrice = model.FinalPrice,
                LockPattern = model.LockPattern,
                Other = model.Other,
                Pin = model.Pin,
                ServiceAction = model.ServiceAction,
                ServiceProtocolStatusId = model.ServiceProtocolStatusId,
                SimTray = model.SimTray,
                UnlockPass = model.UnlockPass,
                ServicePartsJson = model.ServicePartsJson,
                UserId = userId
            };

            var serviceProtocolResult = await this.serviceProtocolService.CreateServiceProtocol(serviceProtocol);

            return Created(nameof(this.Create), serviceProtocolResult.Id);
        }

        [HttpGet]
        public async Task<IEnumerable<ServiceProtocolListResponseModel>> GetList()
        {
            var serviceprotocols = new List<ServiceProtocolListResponseModel>();
            var resultCollection = await serviceProtocolService.GetServiceProtocolsList(this.User.GetId());
            foreach (var item in resultCollection)
            {
                var result = new ServiceProtocolListResponseModel()
                {
                    Id = item.Id,
                    BrandModel = item.BrandModel,
                    ClientPhone = item.ClientPhone,
                    C_DateTime = item.C_DateTime,
                    DateOfIssue = item.DateOfIssue,
                    FinalPrice = item.FinalPrice,
                    ServiceProtocolStatusId = item.ServiceProtocolStatusId
                };
                serviceprotocols.Add(result);
            }
            return serviceprotocols;
        }

        [Route("{id}")]
        [HttpPut]
        public async Task<ActionResult> Edit(int id, ServiceProtocolRequestModel model)
        {
            if (id != model.Id)
            {

                return BadRequest("Not a valid service protocol id");
            }

            var serviceProtocol = new ServiceProtocol()
            {
                Id = model.Id,
                Bag = model.Bag,
                BrandModel = model.BrandModel,
                Charger = model.Charger,
                ClientEmail = model.ClientEmail,
                ClientPhone = model.ClientPhone,
                Comment = model.Comment,
                DateOfIssue = model.DateOfIssue,
                FaultInformation = model.FaultInformation,
                FinalPrice = model.FinalPrice,
                LockPattern = model.LockPattern,
                Other = model.Other,
                Pin = model.Pin,
                ServiceAction = model.ServiceAction,
                ServiceProtocolStatusId = model.ServiceProtocolStatusId,
                SimTray = model.SimTray,
                UnlockPass = model.UnlockPass,
                ServicePartsJson = model.ServicePartsJson,
                UserId = this.User.GetId()
            };

            var currentProtocol = await serviceProtocolService.GetServiceProtocolById(serviceProtocol.Id);
            if (currentProtocol.UserId != serviceProtocol.UserId)
            {
                return Unauthorized("Not a valid user");
            }

            await serviceProtocolService.UpdateServiceProtocol(serviceProtocol);

            return NoContent();
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult<ServiceProtocolResponseModel>> GetById(int id)
        {
            var serviceProtocol = await serviceProtocolService.GetServiceProtocolById(id);

            if (serviceProtocol == null)
            {
                return NotFound();
            }

            if (serviceProtocol.UserId != this.User.GetId())
            {
                return Unauthorized("Not a valid user");
            }

            var result = new ServiceProtocolResponseModel()
            {
                Id = serviceProtocol.Id,
                ClientPhone = serviceProtocol.ClientPhone,
                ClientEmail = serviceProtocol.ClientEmail,
                BrandModel = serviceProtocol.BrandModel,
                Comment = serviceProtocol.Comment,
                C_DateTime = serviceProtocol.C_DateTime,
                Bag = serviceProtocol.Bag,
                Charger = serviceProtocol.Charger,
                DateOfIssue = serviceProtocol.DateOfIssue,
                FaultInformation = serviceProtocol.FaultInformation,
                FinalPrice = serviceProtocol.FinalPrice,
                LockPattern = serviceProtocol.LockPattern,
                Other = serviceProtocol.Other,
                Pin = serviceProtocol.Pin,
                ServiceAction = serviceProtocol.ServiceAction,
                ServiceProtocolStatusId = serviceProtocol.ServiceProtocolStatusId,
                SimTray = serviceProtocol.SimTray,
                UnlockPass = serviceProtocol.UnlockPass,
                User = serviceProtocol.User,
                ServicePartsJson = serviceProtocol.ServicePartsJson
            };

            return Ok(result);
        }

        [Route(nameof(Search) + "/{input}")]
        [HttpGet]
        public async Task<IEnumerable<ServiceProtocolResponseModel>> Search(string input)
        {
            var serviceprotocols = new List<ServiceProtocolResponseModel>();
            var resultCollection = await serviceProtocolService.Search(input, this.User.GetId());
            foreach (var item in resultCollection)
            {
                var result = new ServiceProtocolResponseModel()
                {
                    Id = item.Id,
                    Bag = item.Bag,
                    BrandModel = item.BrandModel,
                    Charger = item.Charger,
                    ClientEmail = item.ClientEmail,
                    ClientPhone = item.ClientPhone,
                    Comment = item.Comment,
                    C_DateTime = item.C_DateTime,
                    DateOfIssue = item.DateOfIssue,
                    FaultInformation = item.FaultInformation,
                    FinalPrice = item.FinalPrice,
                    LockPattern = item.LockPattern,
                    Other = item.Other,
                    Pin = item.Pin,
                    ServiceAction = item.ServiceAction,
                    ServiceProtocolStatusId = item.ServiceProtocolStatusId,
                    SimTray = item.SimTray,
                    UnlockPass = item.UnlockPass,
                    User = item.User
                };
                serviceprotocols.Add(result);
            }
            return serviceprotocols;
        }

        [Route("{id}/{statusid}")]
        [HttpPut]
        public async Task<ActionResult> SetStatus(int id, int statusId)
        {
            var protocol = await this.serviceProtocolService.GetServiceProtocolById(id);
            if (protocol.UserId != this.User.GetId())
            {
                return Unauthorized("Not a valid user");
            }
            await serviceProtocolService.SetServiceProtocolStatus(id, statusId);
            return Accepted("The status was updated.");
        }

        [HttpDelete("{protocolId}")]
        public async Task<ActionResult> DeleteServiceProtocol(int protocolId) 
        {
            if (protocolId == 0)
            {
                return BadRequest("Not a valid service protocol id");
            }

            var protocol = await this.serviceProtocolService.GetServiceProtocolById(protocolId);
            if (protocol.UserId != this.User.GetId())
            {
                return Unauthorized("Not a valid user");
            }

            var currentServiceProtocol = await serviceProtocolService.GetServiceProtocolById(protocolId);
            if (currentServiceProtocol == null)
            {
                return NotFound($"The service protocol {protocolId} not exist.");
            }

            await serviceProtocolService.SetServiceProtocolDeleteFlag(protocolId, 1);
            return NoContent();
        }
    }
}
