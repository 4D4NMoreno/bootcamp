using Core.Interfaces.Services;
using Core.Request;
using Core.Requests;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class PromotionController : BaseApiController
    {
        private readonly IPromotionService _service;

        public PromotionController(IPromotionService service)
        {
            _service = service;
        }
        //[HttpPost("Crear")]
        //public async Task<IActionResult> Create([FromBody] CreatePromotionModel model)
        //{
        //    return Ok(await _service.Add(model));
        //}
        //[HttpPut("Update")]
        //public async Task<IActionResult> Update([FromBody] UpdatePromotionModel model)
        //{
        //    return Ok(await _service.Update(model));
        //}
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete([FromRoute] int id)
        //{
        //    return Ok(await _service.Delete(id));
        //}
        //[HttpGet("GetAll")]
        //public async Task<IActionResult> GetAll()
        //{
        //    return Ok(await _service.GetAll());
        //}
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetById([FromRoute] int id)
        //{
        //    var customer = await _service.GetById(id);
        //    return Ok(customer);
        //}

    }
}