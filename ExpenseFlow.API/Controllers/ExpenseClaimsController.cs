using ExpenseFlow.Application.ExpenseClaims;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseFlow.API.Controllers
{
    public class ExpenseClaimsController : CustomBaseController
    {
        private readonly IExpenseClaimService _expenseClaimService;

        public ExpenseClaimsController(IExpenseClaimService expenseClaimService)
        {
            _expenseClaimService = expenseClaimService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll() => CreateActionResult(await _expenseClaimService.GetAllListAsync());


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id) => CreateActionResult(await _expenseClaimService.GetByIdAsync(id));


        [HttpPost]
        public async Task<IActionResult> Create(ExpenseClaimRequest request)
        {
            return CreateActionResult(await _expenseClaimService.CreateAsync(request));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id,ExpenseClaimRequest request) =>
            CreateActionResult(await _expenseClaimService.UpdateAsync(id, request));


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id) => CreateActionResult(await _expenseClaimService.DeleteAsync(id));

    }
}
