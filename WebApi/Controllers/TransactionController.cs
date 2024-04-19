using Core.Interfaces.Repositories;
using Core.Request;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class TransactionController : BaseApiController
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionController(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        [HttpPost("make-transfer")]
        public async Task<IActionResult> MakeTransfer([FromBody] TransferRequest transferRequest)
        {
            var success = await _transactionRepository.MakeTransfer(transferRequest);

            if (success)
            {
                return Ok("Transferencia realizada con éxito.");
            }
            else
            {
                return BadRequest("No se pudo realizar la transferencia. Por favor, verifique los detalles y vuelva a intentarlo.");
            }
        }
    }
}
