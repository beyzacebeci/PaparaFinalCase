using ExpenseFlow.Application.ExpenseCategories;
using ExpenseFlow.Application.ExpenseClaims;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseFlow.API.Controllers
{

    public class ExpenseCategoriesController : CustomBaseController
    {
        private readonly IExpenseCategoryService _expenseCategoryService;

        public ExpenseCategoriesController(IExpenseCategoryService expenseCategoryService)
        {
            _expenseCategoryService = expenseCategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => CreateActionResult(await _expenseCategoryService.GetAllListAsync());


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id) => CreateActionResult(await _expenseCategoryService.GetByIdAsync(id));


        [HttpPost]
        public async Task<IActionResult> Create(ExpenseCategoryRequest request)
        {
            return CreateActionResult(await _expenseCategoryService.CreateAsync(request));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, ExpenseCategoryRequest request) =>
            CreateActionResult(await _expenseCategoryService.UpdateAsync(id, request));


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id) => CreateActionResult(await _expenseCategoryService.DeleteAsync(id));
    }
}
