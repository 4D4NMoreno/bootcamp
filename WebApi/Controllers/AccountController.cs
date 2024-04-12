using Core.Interfaces.Services;
using Core.Request;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpPost("Crear")]
        public async Task<IActionResult> Create([FromBody] CreateAccountModel request)
        {
            return Ok(await _accountService.Add(request));
        }
    }
}
