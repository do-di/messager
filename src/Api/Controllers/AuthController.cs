using Application.Interface.Service;
using Application.Usecase.Login;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : BaseController
    {
        private readonly ICurrentUserService _currentUserService;
        public AuthController(IMediator mediator, ICurrentUserService currentUserService) : base(mediator)
        {
            _currentUserService = currentUserService;
        }

        [HttpPost(Name = "Login")]
        public async Task<IActionResult> Login(LoginCommand request)
        {
            var result = await mediator.Send(request).ConfigureAwait(false);
            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        public IActionResult CheckAuth()
        {
            var userId = _currentUserService.GetUserId();
            return Ok("Authorized");
        }

        [HttpGet]
        [Route("/")]
        public string GetCurrentIp()
        {
            // test
            string hostName = Dns.GetHostName();

            var ip = Dns.GetHostAddresses(hostName).FirstOrDefault();
            return ip != null ? ip.ToString() : string.Empty;

        }
    }
}
