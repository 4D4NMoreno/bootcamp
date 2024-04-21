using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Request;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class TransactionController : BaseApiController
    {
        private readonly ITransactionService _service;

        public TransactionController(ITransactionService service)
        {
            _service = service;
        }

        [HttpPost("make-transfer")]
        public async Task<IActionResult> MakeTransfer([FromBody] TransferRequest transferRequest)
        {
            return Ok(await _service.MakeTransfer(transferRequest));
        }

        //[HttpPost("make-payment")]
        //public async Task<IActionResult> MakePayment(PaymentRequest paymentRequest)
        //{
            //var result = await -_service.MakePayment(paymentRequest);

            //if (result)
            //{
            //    return Ok("Payment successful.");
            //}
            //else
            //{
            //    return BadRequest("Payment failed.");
            //}

        //}
    }
}
