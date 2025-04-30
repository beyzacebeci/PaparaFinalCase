using AutoMapper;
using ExpenseFlow.DataAccess.ExpenseClaims;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace ExpenseFlow.Application.ExpenseClaims;

public class ExpenseClaimService : IExpenseClaimService
{
    private readonly IExpenseClaimRepository _expenseClaimRepository;
    private readonly IMapper _mapper;

    public ExpenseClaimService(IExpenseClaimRepository expenseClaimRepository, IMapper mapper)
    {
        _expenseClaimRepository = expenseClaimRepository;
        _mapper = mapper;
    }

    public Task<ServiceResult<ExpenseClaimResponse>> CreateAsync(ExpenseClaimRequest request)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResult> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResult<List<ExpenseClaimResponse>>> GetAllListAsync()
    {
        var expenseclaims = await _expenseClaimRepository.GetAll().ToListAsync();
        var expenseClaimsAsDto = _mapper.Map<List<ExpenseClaimResponse>>(expenseclaims);

        return ServiceResult<List<ExpenseClaimResponse>>.Success(expenseClaimsAsDto);
    }

    public async Task<ServiceResult<ExpenseClaimResponse?>> GetByIdAsync(int id)
    {
        var expenseClaim = await _expenseClaimRepository.GetByIdAsync(id);


        if (expenseClaim is null)
        {
            return ServiceResult<ExpenseClaimResponse?>.Fail("Expense claim not found", HttpStatusCode.NotFound);
        }


        var expenseClaimAsDto = _mapper.Map<ExpenseClaimResponse>(expenseClaim);

        return ServiceResult<ExpenseClaimResponse>.Success(expenseClaimAsDto)!;
    }

    public Task<ServiceResult> UpdateAsync(int id, ExpenseClaimRequest request)
    {
        throw new NotImplementedException();
    }
}

