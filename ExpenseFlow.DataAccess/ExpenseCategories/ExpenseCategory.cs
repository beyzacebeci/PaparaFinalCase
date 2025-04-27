using ExpenseFlow.DataAccess.ExpenseClaims;

namespace ExpenseFlow.DataAccess.ExpenseCategories;
public class ExpenseCategory : BaseEntity
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public virtual ICollection<ExpenseClaim> ExpenseClaims { get; set; }
}