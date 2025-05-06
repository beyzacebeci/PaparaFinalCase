using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExpenseFlow.Application.Reports.DTOs;

namespace ExpenseFlow.Application.Reports;

public interface IReportService
{
    // User specific reports
    Task<ServiceResult<ExpenseReportDto>> GetUserExpenseReportAsync(string userId);
    
    // Admin reports
    Task<ServiceResult<List<UserExpenseReportDto>>> GetAllUsersExpenseReportAsync();
    Task<ServiceResult<List<CategoryExpenseReportDto>>> GetCategoryExpenseReportAsync();
    Task<ServiceResult<ExpenseReportDto>> GetOverallExpenseReportAsync();

    // Time-based reports
    Task<ServiceResult<ExpenseReportDto>> GetDailyExpenseReportAsync(DateTime date);
    Task<ServiceResult<ExpenseReportDto>> GetWeeklyExpenseReportAsync(DateTime startOfWeek);
    Task<ServiceResult<ExpenseReportDto>> GetMonthlyExpenseReportAsync(int year, int month);
}