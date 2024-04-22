using Core.Constants;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Models;
using Core.Request;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _repository;

    public TransactionService(ITransactionRepository repository)
    {
        _repository = repository;
    }
    public async Task<TransferDTO> MakeTransfer(TransferRequest transferRequest)
    {
        var validationResult = await _repository.ValidateTransferRequest(transferRequest);

        if (!validationResult.isValid)
        {
            throw new BusinessLogicException(validationResult.message);
        }
        return await _repository.MakeTransfer(transferRequest);
       
    }
    public async Task<PaymentDTO> MakePayment(PaymentRequest paymentRequest)
    {
        var validationResult = await _repository.ValidatePaymentRequest(paymentRequest);

        if (!validationResult.isValid)
        {
            throw new BusinessLogicException(validationResult.message);
        }
        return await _repository.MakePayment(paymentRequest);
    }

    public async Task<DepositDTO> MakeDeposit(DepositRequest DepositRequest)
    {
        var validationResult = await _repository.ValidateDepositRequest(DepositRequest);

        if (!validationResult.isValid)
        {
            throw new BusinessLogicException(validationResult.message);
        }
        return await _repository.MakeDeposit(DepositRequest);
    }

    public async Task<WithdrawalDTO> MakeWithdrawal(WithdrawalRequest withdrawalRequest)
    {
        var validationResult = await _repository.ValidateWithdrawalRequest(withdrawalRequest);

        if (!validationResult.isValid)
        {
            throw new BusinessLogicException(validationResult.message);
        }
        return await _repository.MakeWithdrawal(withdrawalRequest);
    }
}
