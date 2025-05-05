using ExpenseFlow.Application.ExpenseCategories;
using ExpenseFlow.Application.PaymentTransactions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseFlow.API.Controllers;

[Authorize(Roles = "Admin")]
public class PaymentTransactionsController : CustomBaseController
{
    private readonly IPaymentTransactionService _paymentTransactionService;

    public PaymentTransactionsController(IPaymentTransactionService expenseCategoryService)
    {
        _paymentTransactionService = expenseCategoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() => CreateActionResult(await _paymentTransactionService.GetAllListAsync());
}

