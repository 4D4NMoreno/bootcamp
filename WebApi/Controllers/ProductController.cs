using Core.Constants;
using Core.Interfaces.Services;
using Core.Request;
using Core.Requests;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class ProductRequestController : BaseApiController
    {
        private readonly IProductService _service;

        public ProductRequestController(IProductService service)
        {
            _service = service;
        }

        [HttpPost("Crear")]
        public async Task<IActionResult> Create([FromBody] CreateProductRequest request)
        {
            return Ok(await _service.Add(request));
        }

    }
}
