namespace ExpenseFlow.Application.ExpenseCategories;

public record ExpenseCategoryRequest
{
    public string Name { get; init; } = default!;

}

