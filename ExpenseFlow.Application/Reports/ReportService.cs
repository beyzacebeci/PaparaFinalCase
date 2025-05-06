using ExpenseFlow.Application.Reports.DTOs;
using ExpenseFlow.DataAccess.ExpenseCategories;
using ExpenseFlow.DataAccess.ExpenseClaims;
using ExpenseFlow.DataAccess.PaymentTransactions;
using ExpenseFlow.DataAccess.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ExpenseFlow.Application.Reports;

public class ReportService : IReportService
{
    private readonly IPaymentTransactionRepository _paymentTransactionRepository;
    private readonly IExpenseClaimRepository _expenseClaimRepository;
    private readonly IExpenseCategoryRepository _expenseCategoryRepository;
    private readonly UserManager<User> _userManager;

    public ReportService(IExpenseCategoryRepository expenseCategoryRepository, IExpenseClaimRepository expenseClaimRepository, IPaymentTransactionRepository paymentTransactionRepository, UserManager<User> userManager)
    {
        _expenseCategoryRepository = expenseCategoryRepository;
        _expenseClaimRepository = expenseClaimRepository;
        _paymentTransactionRepository = paymentTransactionRepository;
        _userManager = userManager;
    }

    public async Task<ServiceResult<ExpenseReportDto>> GetUserExpenseReportAsync(string userId)
    {
        var claims = await _expenseClaimRepository
            .Where(x => x.UserId == userId)
            .ToListAsync();

        return ServiceResult<ExpenseReportDto>.Success(GenerateExpenseReport(claims));
    }

    public async Task<ServiceResult<List<UserExpenseReportDto>>> GetAllUsersExpenseReportAsync()
    {
        var users = await _userManager.Users
            .Include(u => u.ExpenseClaims)
            .Where(u => u.ExpenseClaims.Any())
            .ToListAsync();

        var reports = new List<UserExpenseReportDto>();

        foreach (var user in users)
        {
            var userClaims = user.ExpenseClaims.ToList();
            var report = GenerateExpenseReport(userClaims);

            reports.Add(new UserExpenseReportDto
            {
                UserId = user.Id,
                UserName = $"{user.FirstName} {user.LastName}",
                UserEmail = user.Email,
                TotalAmount = report.TotalAmount,
                TotalClaims = report.TotalClaims,
                ApprovedClaims = report.ApprovedClaims,
                RejectedClaims = report.RejectedClaims,
                PendingClaims = report.PendingClaims,
                PaidClaims = report.PaidClaims,
                ApprovedAmount = report.ApprovedAmount,
                RejectedAmount = report.RejectedAmount,
                PendingAmount = report.PendingAmount,
                PaidAmount = report.PaidAmount
            });
        }

        return ServiceResult<List<UserExpenseReportDto>>.Success(reports);
    }


    public async Task<ServiceResult<List<CategoryExpenseReportDto>>> GetCategoryExpenseReportAsync()
    {
        var categories = await _expenseCategoryRepository.GetAllIncludingClaimsAsync();

        var reports = new List<CategoryExpenseReportDto>();

        foreach (var category in categories)
        {
            var categoryClaims = category.ExpenseClaims.ToList();
            var report = GenerateExpenseReport(categoryClaims);

            reports.Add(new CategoryExpenseReportDto
            {
                CategoryId = category.Id,
                CategoryName = category.Name,
                TotalAmount = report.TotalAmount,
                TotalClaims = report.TotalClaims,
                ApprovedClaims = report.ApprovedClaims,
                RejectedClaims = report.RejectedClaims,
                PendingClaims = report.PendingClaims,
                PaidClaims = report.PaidClaims,
                ApprovedAmount = report.ApprovedAmount,
                RejectedAmount = report.RejectedAmount,
                PendingAmount = report.PendingAmount,
                PaidAmount = report.PaidAmount
            });
        }

        return ServiceResult<List<CategoryExpenseReportDto>>.Success(reports);
    }

    public async Task<ServiceResult<ExpenseReportDto>> GetOverallExpenseReportAsync()
    {
        var claims = await _expenseClaimRepository
            .GetAll() // veya .AsQueryable() ya da doðrudan repository'den tümünü çek
            .ToListAsync();

        return ServiceResult<ExpenseReportDto>.Success(GenerateExpenseReport(claims));
    }

    public async Task<ServiceResult<ExpenseReportDto>> GetDailyExpenseReportAsync(DateTime date)
    {
        var startDate = date.Date;
        var endDate = startDate.AddDays(1).AddSeconds(-1);

        var claims = await _expenseClaimRepository
            .Where(x => x.ExpenseDate >= startDate && x.ExpenseDate <= endDate)
            .ToListAsync();

        return ServiceResult<ExpenseReportDto>.Success(GenerateExpenseReport(claims));
    }

    public async Task<ServiceResult<ExpenseReportDto>> GetWeeklyExpenseReportAsync(DateTime startOfWeek)
    {
        var endOfWeek = startOfWeek.AddDays(7).AddSeconds(-1);

        var claims = await _expenseClaimRepository
            .Where(x => x.ExpenseDate >= startOfWeek && x.ExpenseDate <= endOfWeek)
            .ToListAsync();

        return ServiceResult<ExpenseReportDto>.Success(GenerateExpenseReport(claims));
    }

    public async Task<ServiceResult<ExpenseReportDto>> GetMonthlyExpenseReportAsync(int year, int month)
    {
        var startDate = new DateTime(year, month, 1);
        var endDate = startDate.AddMonths(1).AddSeconds(-1);

        var claims = await _expenseClaimRepository
            .Where(x => x.ExpenseDate >= startDate && x.ExpenseDate <= endDate)
            .ToListAsync();

        return ServiceResult<ExpenseReportDto>.Success(GenerateExpenseReport(claims));
    }

    private ExpenseReportDto GenerateExpenseReport(List<ExpenseClaim> claims)
    {
        return new ExpenseReportDto
        {
            TotalClaims = claims.Count,
            TotalAmount = claims.Sum(x => x.Amount),
            ApprovedClaims = claims.Count(x => x.Status == ExpenseStatus.Approved),
            RejectedClaims = claims.Count(x => x.Status == ExpenseStatus.Rejected),
            PendingClaims = claims.Count(x => x.Status == ExpenseStatus.Pending),
            PaidClaims = claims.Count(x => x.Status == ExpenseStatus.Paid),
            ApprovedAmount = claims.Where(x => x.Status == ExpenseStatus.Approved).Sum(x => x.Amount),
            RejectedAmount = claims.Where(x => x.Status == ExpenseStatus.Rejected).Sum(x => x.Amount),
            PendingAmount = claims.Where(x => x.Status == ExpenseStatus.Pending).Sum(x => x.Amount),
            PaidAmount = claims.Where(x => x.Status == ExpenseStatus.Paid).Sum(x => x.Amount)
        };
    }
}