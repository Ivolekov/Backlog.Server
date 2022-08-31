namespace Backlog.Server.Features.ServiceProtocols
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Backlog.Server.Utilities;
    using Backlog.Server.Data.Models;
    using System.Collections.Generic;
    using Backlog.Server.Infrastructure;
    using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> Create(IServiceProtocol model)
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
        public async Task<ActionResult<IEnumerable<ServiceProtocolListResponseModel>>> GetList()
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
            return Ok(serviceprotocols);
        }

        [Route("{id}")]
        [HttpPut]
        public async Task<IActionResult> Edit(int id, IServiceProtocol model)
        {
            if (id != model.Id)
            {

                return BadRequest(Messages.NotValidServiceProtocolId);
            }

            model.UserId = this.User.GetId();

            var currentProtocol = await serviceProtocolService.GetServiceProtocolById(model.Id);
            
            if (currentProtocol.UserId != model.UserId)
            {
                return Unauthorized(Messages.Unauthorized);
            }

            await serviceProtocolService.UpdateServiceProtocol(model);

            return NoContent();
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult<IServiceProtocol>> GetById(int protocolId)
        {
            var serviceProtocol = await serviceProtocolService.GetServiceProtocolById(protocolId);

            if (serviceProtocol == null)
            {
                return NotFound(string.Format(Messages.NotFoundWithId, protocolId));
            }
            else if (serviceProtocol.UserId != this.User.GetId())
            {
                return Unauthorized(Messages.Unauthorized);
            }

            return Ok(serviceProtocol);
        }

        [Route(nameof(Search) + "/{input}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceProtocolListResponseModel>>> Search(string input)
        {
            var serviceprotocols = new List<ServiceProtocolListResponseModel>();
            var resultCollection = await serviceProtocolService.Search(input, this.User.GetId());

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
           
            return Ok(serviceprotocols);
        }

        [Route("{id}/{statusid}")]
        [HttpPut]
        public async Task<ActionResult> SetStatus(int serviceProtocolId, int serviceProtocolStatusId)
        {
            var protocol = await this.serviceProtocolService.GetServiceProtocolById(serviceProtocolId);
            
            if (protocol.UserId != this.User.GetId())
            {
                return Unauthorized(Messages.Unauthorized);
            }

            await serviceProtocolService.SetServiceProtocolStatus(serviceProtocolId, serviceProtocolStatusId);

            return Accepted(Messages.StatusWasUpdated);
        }

        [HttpDelete("{protocolId}")]
        public async Task<IActionResult> DeleteServiceProtocol(int protocolId)
        {
            if (protocolId == 0)
            {
                return BadRequest(Messages.NotValidServiceProtocolId);
            }

            var currentServiceProtocol = await this.serviceProtocolService.GetServiceProtocolById(protocolId);
           
            if (currentServiceProtocol == null)
            {
                return NotFound($"The service protocol {protocolId} not exist.");
            }
            else if (currentServiceProtocol.UserId != this.User.GetId())
            {
                return Unauthorized(Messages.Unauthorized);
            }

            await serviceProtocolService.SetServiceProtocolDeleteFlag(protocolId, 1);
            
            return NoContent();
        }
    }
}
