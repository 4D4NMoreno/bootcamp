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

        [HttpPost("make-payment")]
        public async Task<IActionResult> MakePayment(PaymentRequest paymentRequest)
        {
            return Ok(await _service.MakePayment(paymentRequest));

        }
        [HttpPost("make-deposit")]
        public async Task<IActionResult> MakeDeposit(DepositRequest depositRequest)
        {
            return Ok(await _service.MakeDeposit(depositRequest));

        }
        [HttpPost("make-withdrawal")]
        public async Task<IActionResult> MakeWithdrawal(WithdrawalRequest withdrawalRequest)
        {
            return Ok(await _service.MakeWithdrawal(withdrawalRequest));

        }
    }
}
