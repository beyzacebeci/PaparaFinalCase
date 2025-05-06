using ExpenseFlow.Application.Reports;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ExpenseFlow.API.Controllers;

[Authorize]
public class ReportsController : CustomBaseController
{
    private readonly IReportService _reportService;

    public ReportsController(IReportService reportService)
    {
        _reportService = reportService;
    }

    // Personel için kendi masraf raporu
    [Authorize(Roles = "Employee")]

    [HttpGet("user")]
    public async Task<IActionResult> GetUserExpenseReport()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
            return Unauthorized();

        var result = await _reportService.GetUserExpenseReportAsync(userId);
        return Ok(result);
    }



    // Admin için tüm kullanıcıların raporu
    [Authorize(Roles = "Admin")]
    [HttpGet("all-users")]
    public async Task<IActionResult> GetAllUsersReport()
    {
        return CreateActionResult(await _reportService.GetAllUsersExpenseReportAsync());
    }

    // Admin için kategori bazlı rapor
    [Authorize(Roles = "Admin")]
    [HttpGet("categories")]
    public async Task<IActionResult> GetCategoryReport()
    {
        return CreateActionResult(await _reportService.GetCategoryExpenseReportAsync());
    }

    // Admin için genel rapor
    [Authorize(Roles = "Admin")]
    [HttpGet("overall")]
    public async Task<IActionResult> GetOverallReport()
    {
        return CreateActionResult(await _reportService.GetOverallExpenseReportAsync());
    }

    // Günlük rapor
    [Authorize(Roles = "Admin")]
    [HttpGet("daily")]
    public async Task<IActionResult> GetDailyReport([FromQuery] DateTime date)
    {
        return CreateActionResult(await _reportService.GetDailyExpenseReportAsync(date));
    }

    // Haftalık rapor
    [Authorize(Roles = "Admin")]
    [HttpGet("weekly")]
    public async Task<IActionResult> GetWeeklyReport([FromQuery] DateTime startOfWeek)
    {
        return CreateActionResult(await _reportService.GetWeeklyExpenseReportAsync(startOfWeek));
    }

    // Aylık rapor
    [Authorize(Roles = "Admin")]
    [HttpGet("monthly")]
    public async Task<IActionResult> GetMonthlyReport([FromQuery] int year, [FromQuery] int month)
    {
        return CreateActionResult(await _reportService.GetMonthlyExpenseReportAsync(year, month));
    }
}