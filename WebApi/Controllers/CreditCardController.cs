using Core.Interfaces.Services;
using Core.Request;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class CreditCardController : BaseApiController
    {
        private readonly ICreditCardService _service;

        public CreditCardController(ICreditCardService service)
        {
            _service = service;
        }

        //[HttpPost]
        //public async Task<IActionResult> Create([FromBody] CreateCreditCardModel request)
        //{
        //    return Ok(await _service.Add(request));
        //}
        //[HttpGet]
        //public async Task<IActionResult> GetAll()
        //{
        //    var creditCard = await _service.GetAll();
        //    return Ok(creditCard);
        //}
        //[HttpPut("Actualizar")]
        //public async Task<IActionResult> Update([FromBody] UpdateCreditCardModel model)
        //{


        //    return Ok(await _service.Update(model));


        //}
    }
}
