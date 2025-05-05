namespace ExpenseFlow.DataAccess;

public class BaseEntity<T>
{
    public T Id { get; set; } = default!;

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedDate { get; set; }
    public bool IsActive { get; set; } = true;

}


