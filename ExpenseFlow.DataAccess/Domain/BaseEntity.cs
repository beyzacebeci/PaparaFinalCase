namespace ExpenseFlow.DataAccess.Domain;

public class BaseEntity
{
    public long Id { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public bool IsActive { get; set; }

}


