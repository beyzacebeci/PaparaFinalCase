namespace ExpenseFlow.DataAccess;

public class BaseEntity<T>
{
    public T Id { get; set; } = default!;
    //public long Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public bool IsActive { get; set; }

}


