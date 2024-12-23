using Application.Usecase.Login;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : BaseController
    {
        public AuthController(IMediator mediator) : base(mediator)
        {
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
            return Ok("Authorized");
        }
    }
}
