using ExpenseFlow.Application.ExpenseClaims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ExpenseFlow.API.Controllers
{
    //[Authorize]
    public class ExpenseClaimsController : CustomBaseController
    {
        private readonly IExpenseClaimService _expenseClaimService;

        public ExpenseClaimsController(IExpenseClaimService expenseClaimService)
        {
            _expenseClaimService = expenseClaimService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAll() => CreateActionResult(await _expenseClaimService.GetAllListAsync());

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id) => CreateActionResult(await _expenseClaimService.GetByIdAsync(id));

        [Authorize(Roles = "Employee,Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(ExpenseClaimRequest request)
        {
            return CreateActionResult(await _expenseClaimService.CreateAsync(request));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, ExpenseClaimRequest request) =>
            CreateActionResult(await _expenseClaimService.UpdateAsync(id, request));

        
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id) => CreateActionResult(await _expenseClaimService.DeleteAsync(id));

        //bu endpoint ile employee yalnizca kendi masraflarını gorur.
        [Authorize(Roles = "Employee")]
        [HttpGet("user-claims")]
        public async Task<IActionResult> GetMyClaims()
        {
            return CreateActionResult(await _expenseClaimService.GetListByCurrentUserAsync());
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] ExpenseClaimStatusUpdateRequest request)
        {
            return CreateActionResult(await _expenseClaimService.UpdateExpenseClaimStatusAsync(id, request.Status, request.ExpenseStatusDescription));
        }
    }
}
