using AutoMapper;
using ExpenseFlow.Application.ExpenseCategories;
using ExpenseFlow.DataAccess.AppUnitOfWork;
using ExpenseFlow.DataAccess.PaymentTransactions;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace ExpenseFlow.Application.PaymentTransactions;

public class PaymentTransactionService : IPaymentTransactionService
{
    private readonly IPaymentTransactionRepository _paymentTransactionRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public PaymentTransactionService(IPaymentTransactionRepository paymentTransactionRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _paymentTransactionRepository = paymentTransactionRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<ServiceResult<List<PaymentTransactionResponse>>> GetAllListAsync()
    {
        var transactions = await _paymentTransactionRepository.GetAll().ToListAsync();
        var transactionsAsDto = _mapper.Map<List<PaymentTransactionResponse>>(transactions);

        return ServiceResult<List<PaymentTransactionResponse>>.Success(transactionsAsDto);
    }

    public async Task<ServiceResult> CreateAsync(PaymentTransactionRequest request)
    {

        var paymentTransaction = _mapper.Map<PaymentTransaction>(request);

        await _paymentTransactionRepository.AddAsync(paymentTransaction);
        await _unitOfWork.SaveChangesAsync();
        return ServiceResult.Success(HttpStatusCode.NoContent);

    }

}

