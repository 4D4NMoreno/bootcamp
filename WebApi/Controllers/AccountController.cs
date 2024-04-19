using Core.Interfaces.Services;
using Core.Request;
using Core.Requests;
using Infrastructure.Services;
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
        public async Task<IActionResult> Create([FromBody] CreateAccountRequest request)
        {
            return Ok(await _accountService.Add(request));
        }
        //[HttpGet("Filtrar")]
        //public async Task<IActionResult> GetFiltered([FromQuery] FilterAccountModel filter)
        //{
        //    var accounts = await _accountService.GetFiltered(filter);
        //    return Ok(accounts);
        //}
    }
}
