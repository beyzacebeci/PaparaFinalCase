using AutoMapper;
using ExpenseFlow.Application.PaymentTransactions;
using ExpenseFlow.DataAccess.AppUnitOfWork;
using ExpenseFlow.DataAccess.ExpenseClaims;
using ExpenseFlow.DataAccess.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Claims;

namespace ExpenseFlow.Application.ExpenseClaims;

public class ExpenseClaimService : IExpenseClaimService
{
    private readonly IExpenseClaimRepository _expenseClaimRepository;
    private readonly IPaymentTransactionService _paymentTransactionService;

    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly UserManager<User> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;


    public ExpenseClaimService(
        IExpenseClaimRepository expenseClaimRepository,
        IMapper mapper,
        IUnitOfWork unitOfWork,
        IHttpContextAccessor httpContextAccessor,
        UserManager<User> userManager,
        IPaymentTransactionService paymentTransactionService)
    {
        _expenseClaimRepository = expenseClaimRepository;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
        _unitOfWork = unitOfWork;
        _userManager = userManager;
        _paymentTransactionService = paymentTransactionService;
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

    public async Task<ServiceResult> CreateAsync(ExpenseClaimRequest request)
    {
        var expenseClaim = _mapper.Map<ExpenseClaim>(request);

        // Kullanıcı ID'sini al
        var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
        {
            return ServiceResult.Fail("Kullanıcı kimliği alınamadı", HttpStatusCode.Unauthorized);
        }

        expenseClaim.UserId = userId;
        expenseClaim.PaymentReference = $"CLM-{expenseClaim.Id}-{Guid.NewGuid().ToString().Substring(0, 8)}";

        await _expenseClaimRepository.AddAsync(expenseClaim);
        await _unitOfWork.SaveChangesAsync();

        return ServiceResult.Success(HttpStatusCode.NoContent);


    }


    public async Task<ServiceResult> UpdateAsync(int id, ExpenseClaimRequest request)
    {
        var expenseClaim = await _expenseClaimRepository.GetByIdAsync(id);
        if (expenseClaim == null)
        {
            return ServiceResult.Fail("Expense claim not found", HttpStatusCode.NotFound);
        }

        _mapper.Map(request, expenseClaim);
        expenseClaim.UpdatedDate = DateTime.UtcNow;
        _expenseClaimRepository.Update(expenseClaim);
        await _unitOfWork.SaveChangesAsync();

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }

    public async Task<ServiceResult> DeleteAsync(int id)
    {
        var expenseClaim = await _expenseClaimRepository.GetByIdAsync(id);
        if (expenseClaim == null)
        {
            return ServiceResult.Fail("Expense claim not found", HttpStatusCode.NotFound);
        }
       
        expenseClaim.UpdatedDate = DateTime.UtcNow;
        _expenseClaimRepository.Delete(expenseClaim);
        await _unitOfWork.SaveChangesAsync();
        return ServiceResult.Success(HttpStatusCode.NoContent);
    }

    public async Task<ServiceResult<List<ExpenseClaimResponse>>> GetListByCurrentUserAsync()
    {
        var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            return ServiceResult<List<ExpenseClaimResponse>>.Fail("Kullanıcı kimliği alınamadı", HttpStatusCode.Unauthorized);
        }

        var expenseClaims = await _expenseClaimRepository
            .Where(x => x.UserId == userId)
            .ToListAsync();

        var dto = _mapper.Map<List<ExpenseClaimResponse>>(expenseClaims);

        return ServiceResult<List<ExpenseClaimResponse>>.Success(dto);
    }

    private async Task<bool> IsAdmin()
    {
        var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            return false; // Kullanıcı kimliği bulunamadıysa admin değil
        }

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return false; // Kullanıcı bulunamadıysa admin değil
        }

        var roles = await _userManager.GetRolesAsync(user);
        return roles.Contains("Admin"); // Kullanıcı admin rolüne sahipse true döner
    }

    public async Task<ServiceResult> UpdateExpenseClaimStatusAsync(int id, ExpenseStatus newStatus, string? statusDescription = null)
    {
        var expenseClaim = await _expenseClaimRepository.GetByIdAsync(id);
        if (expenseClaim == null)
        {
            return ServiceResult.Fail("Expense claim not found", HttpStatusCode.NotFound);
        }

        if (!await IsAdmin())
        {
            return ServiceResult.Fail("You do not have permission to change the status", HttpStatusCode.Forbidden);
        }

        if (newStatus == ExpenseStatus.Rejected && string.IsNullOrEmpty(statusDescription))
        {
            return ServiceResult.Fail("Rejection reason is required when rejecting a claim", HttpStatusCode.BadRequest);
        }

        expenseClaim.Status = newStatus;
        expenseClaim.ExpenseStatusDescription = statusDescription;

        if (newStatus == ExpenseStatus.Approved)
        {
            expenseClaim.ApprovalDate = DateTime.UtcNow;

            var paymentRequest = new PaymentTransactionRequest
            {
                ExpenseClaimId = expenseClaim.Id,
                UserId = expenseClaim.UserId!,
                Amount = expenseClaim.Amount,
                PaymentDate = DateTime.UtcNow,
                PaymentReference = expenseClaim.PaymentReference,
                PaymentStatus = "Success" 
            };

            await _paymentTransactionService.CreateAsync(paymentRequest);
        }

        _expenseClaimRepository.Update(expenseClaim);
        await _unitOfWork.SaveChangesAsync();

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }

}

