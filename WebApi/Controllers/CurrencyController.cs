using Core.Entities;
using Core.Interfaces.Services;
using Core.Models;
using Core.Request;
using Core.Requests;
using Infrastructure.Services;
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
        [HttpGet("filtrar")]
        public async Task<IActionResult> GetFiltered([FromQuery] FilterCurrencyModel filter)
        {
            var currency = await _service.GetFiltered(filter);
            return Ok(currency);
        }
        [HttpPost("{id}")]
        public async Task<IActionResult> GetById(int id)
        {

            var currency = await _service.GetById(id);
            return Ok(currency);

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
           return Ok(await _service.Delete(id));
        }
        [HttpPut("Actualizar")]
        public async Task<IActionResult> Update([FromBody] UpdateCurrencyModel request)
        {
            return Ok(await _service.Update(request));
        }
    }
}

