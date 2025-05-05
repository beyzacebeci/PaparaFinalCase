using AutoMapper;
using ExpenseFlow.DataAccess.AppUnitOfWork;
using ExpenseFlow.DataAccess.ExpenseCategories;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace ExpenseFlow.Application.ExpenseCategories;

public class ExpenseCategoryService : IExpenseCategoryService
{
    private readonly IExpenseCategoryRepository _expenseCategoryRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public ExpenseCategoryService(IExpenseCategoryRepository expenseCategoryRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _expenseCategoryRepository = expenseCategoryRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<ServiceResult<List<ExpenseCategoryResponse>>> GetAllListAsync()
    {
        var categories = await _expenseCategoryRepository.GetAll().ToListAsync();
        var categoriesAsDto = _mapper.Map<List<ExpenseCategoryResponse>>(categories);

        return ServiceResult<List<ExpenseCategoryResponse>>.Success(categoriesAsDto);
    }

    public async Task<ServiceResult<ExpenseCategoryResponse?>> GetByIdAsync(int id)
    {
        var expenseCategory = await _expenseCategoryRepository.GetByIdAsync(id);


        if (expenseCategory is null)
        {
            return ServiceResult<ExpenseCategoryResponse?>.Fail("Category not found", HttpStatusCode.NotFound);
        }


        var categoryAsDto = _mapper.Map<ExpenseCategoryResponse>(expenseCategory);

        return ServiceResult<ExpenseCategoryResponse>.Success(categoryAsDto)!;
    }

    public async Task<ServiceResult> CreateAsync(ExpenseCategoryRequest request)
    {

        var category = _mapper.Map<ExpenseCategory>(request);

        await _expenseCategoryRepository.AddAsync(category);
        await _unitOfWork.SaveChangesAsync();
        return ServiceResult.Success(HttpStatusCode.NoContent);

    }

    public async Task<ServiceResult> UpdateAsync(int id, ExpenseCategoryRequest request)
    {
        var existingCategory = await _expenseCategoryRepository.GetByIdAsync(id);
        if (existingCategory == null)
        {
            return ServiceResult.Fail("Category not found", HttpStatusCode.NotFound);
        }

        _mapper.Map(request, existingCategory);
        existingCategory.UpdatedDate = DateTime.UtcNow;

        _expenseCategoryRepository.Update(existingCategory);
        await _unitOfWork.SaveChangesAsync();

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }

    public async Task<ServiceResult> DeleteAsync(int id)
    {
        var category = await _expenseCategoryRepository.GetByIdAsync(id);
        if (category == null)
        {
            return ServiceResult.Fail("Category not found", HttpStatusCode.NotFound);
        }

        category.UpdatedDate = DateTime.UtcNow;
        _expenseCategoryRepository.Delete(category);
        await _unitOfWork.SaveChangesAsync();
        return ServiceResult.Success(HttpStatusCode.NoContent);
    }
}

