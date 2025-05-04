namespace ExpenseFlow.Application.ExpenseCategories;

public record ExpenseCategoryResponse
{
    public int Id { get; init; }
    public string Name { get; init; } = default!;

}

