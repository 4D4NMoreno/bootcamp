using Core.Interfaces.Services;
using Core.Models;
using Core.Request;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class EnterpriseController : BaseApiController
    {
        private readonly IEnterpriseService _service;

        public EnterpriseController(IEnterpriseService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEnterpriseModel model)
        {
            return Ok(await _service.Add(model));
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAll());
        }
    }
}
