namespace ExpenseFlow.Application.Reports.DTOs;

    public class CategoryExpenseReportDto : ExpenseReportDto
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }

