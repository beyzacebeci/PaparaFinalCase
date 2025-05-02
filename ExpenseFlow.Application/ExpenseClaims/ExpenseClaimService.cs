using AutoMapper;
using ExpenseFlow.DataAccess.AppUnitOfWork;
using ExpenseFlow.DataAccess.ExpenseClaims;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace ExpenseFlow.Application.ExpenseClaims;

public class ExpenseClaimService : IExpenseClaimService
{
    private readonly IExpenseClaimRepository _expenseClaimRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public ExpenseClaimService(IExpenseClaimRepository expenseClaimRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _expenseClaimRepository = expenseClaimRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
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

    public async Task<ServiceResult<ExpenseClaimResponse>> CreateAsync(ExpenseClaimRequest request)
    {

        var expenseClaim = _mapper.Map<ExpenseClaim>(request);

        await _expenseClaimRepository.AddAsync(expenseClaim);
        await _unitOfWork.SaveChangesAsync();
        return ServiceResult<ExpenseClaimResponse>.SuccessAsCreated(
            new ExpenseClaimResponse { Id = expenseClaim.Id },
            $"api/expenseclaims/{expenseClaim.Id}"
        );
    }

    public async Task<ServiceResult> UpdateAsync(int id, ExpenseClaimRequest request)
    {
        var product = _mapper.Map<ExpenseClaim>(request);
        product.Id = id;

        _expenseClaimRepository.Update(product);
        await _unitOfWork.SaveChangesAsync();

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }

    public async Task<ServiceResult> DeleteAsync(int id)
    {
        var expenseClaim = await _expenseClaimRepository.GetByIdAsync(id);


        _expenseClaimRepository.Delete(expenseClaim!);
        await _unitOfWork.SaveChangesAsync();
        return ServiceResult.Success(HttpStatusCode.NoContent);
    }

}

