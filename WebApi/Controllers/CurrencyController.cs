using Core.Interfaces.Services;
using Core.Models;
using Core.Request;
using Core.Requests;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class CurrencyController : BaseApiController
    {
        private readonly ICurrencyService _service;

        public CurrencyController(ICurrencyService service)
        {
            _service = service;
        }

        [HttpPost("Crear")]
        public async Task<IActionResult> Create([FromBody] CreateCurrencyModel request)
        {
            return Ok(await _service.Add(request));
        }
    }
}
