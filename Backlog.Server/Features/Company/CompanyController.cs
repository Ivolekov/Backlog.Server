namespace Backlog.Server.Features.Company
{
    using Backlog.Server.Features.Company.Models;
    using Backlog.Server.Infrastructure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using ServiceManager.Models;
    using System;
    using System.Threading.Tasks;

    [Authorize]
    public class CompanyController : ApiController
    {
        private readonly ICompanyService companyService;
        public CompanyController(ICompanyService companyService) => this.companyService = companyService;

        [HttpGet]
        public async Task<CompanyProfileResponseModel> Get()
        {
            var result = await this.companyService.GetCompanyProfile(this.User.GetId());
            var response = new CompanyProfileResponseModel();
            if (result == null)
            {
                response = new CompanyProfileResponseModel()
                {
                    Id = -1,
                    Bulstat = "999999999",
                    CompanyAddress = "CompanyAddress",
                    CompanyEmail = "CompanyEmail",
                    CompanyName = "CompanyName",
                    CompanyOwner = "CompanyOwner",
                    CompanyPhone = "CompanyPhone",
                    CompanyWebAddress = "CompanyWebAddress"
                };
            }
            else
            {
                response = new CompanyProfileResponseModel()
                {
                    Id = result.Id,
                    Bulstat = result.Bulstat,
                    CompanyAddress = result.CompanyAddress,
                    CompanyEmail = result.CompanyEmail,
                    CompanyName = result.CompanyName,
                    CompanyOwner = result.CompanyOwner,
                    CompanyPhone = result.CompanyPhone,
                    CompanyWebAddress = result.CompanyWebAddress
                };
            }

            return response;
        }

        [Route("{id}")]
        [HttpPut]
        public async Task<IActionResult> Edit(int id, CompanyProfileReqestModel model)
        {
            if (id != model.Id)
            {
                return BadRequest("Not a valid company profile id");
            }

            var username = this.User.GetUsername();
            var companyProfile = new CompanyProfile()
            {
                Id = model.Id,
                Bulstat = model.Bulstat,
                CompanyAddress = model.CompanyAddress,
                CompanyEmail = model.CompanyEmail,
                CompanyName = model.CompanyName,
                CompanyOwner = model.CompanyOwner,
                CompanyPhone = model.CompanyPhone,
                CompanyWebAddress = model.CompanyWebAddress,
                C_User = username,
                C_DateTime = DateTime.Now,
                UserId = this.User.GetId()
            };
            if (companyProfile.Id == -1)
            {
                await companyService.CreateCompanyProfile(companyProfile);
            }
            await companyService.UpdateCompanyProfile(companyProfile);

            return NoContent();
        }
    }
}
