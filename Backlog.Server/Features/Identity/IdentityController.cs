namespace Backlog.Server.Features.Identity
{
    using Backlog.Server.Data.Models;
    using Backlog.Server.Features.Identity.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;
    using System;
    using System.Threading.Tasks;

    public class IdentityController : ApiController
    {
        private readonly UserManager<User> userManager;
        private readonly AppSettings appSettings;
        private readonly IIdentityService identityService;

        public IdentityController(UserManager<User> userManager, IOptions<AppSettings> appSettings, IIdentityService identityService)
        {
            this.userManager = userManager;
            this.appSettings = appSettings.Value;
            this.identityService = identityService;
        }

        [HttpPost]
        [Route(nameof(Register))]
        public async Task<ActionResult> Register(RegisterUserRequestModel model)
        {
            var user = new User()
            {
                UserName = model.UserName,
                Email = model.Email
            };

            var result = await this.userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest(result.Errors);
        }

        [HttpPost]
        [Route(nameof(Login))]
        public async Task<ActionResult<LoginResponseModel>> Login(LoginRequestModel model)
        {
            try
            {
                var user = await this.userManager.FindByNameAsync(model.UserName);
                if (user == null)
                {
                    return Unauthorized();
                }

                var passwordValid = await this.userManager.CheckPasswordAsync(user, model.Password);
                if (!passwordValid)
                {
                    return Unauthorized();
                }

                var token = this.identityService.GenerateJwtToken(user.Id, user.UserName, this.appSettings.Secret);

                return new LoginResponseModel
                {
                    Token = token
                };
            }
            catch (Exception ex)
            {
                return new LoginResponseModel
                {
                    Token = "FaildToken"
                };
            }
        }
    }
}
