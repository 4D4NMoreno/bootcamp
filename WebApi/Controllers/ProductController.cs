using Core.Constants;
using Core.Interfaces.Services;
using Core.Request;
using Core.Requests;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class ProductController : BaseApiController
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpPost("Crear")]
        public async Task<IActionResult> Create([FromBody] BankProductRequest request)
        {
            return Ok(await _service.Add(request));
        }

    }
}
