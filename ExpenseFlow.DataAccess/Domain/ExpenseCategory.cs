namespace ExpenseFlow.DataAccess.Domain;
public class ExpenseCategory : BaseEntity
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public virtual ICollection<ExpenseClaim> ExpenseClaims { get; set; }
}