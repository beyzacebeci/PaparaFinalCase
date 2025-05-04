using ExpenseFlow.DataAccess.ExpenseClaims;

namespace ExpenseFlow.DataAccess.ExpenseCategories;
public class ExpenseCategory : BaseEntity<int>
{
    public string Name { get; set; } = default!;
    public virtual ICollection<ExpenseClaim> ExpenseClaims { get; set; }
}