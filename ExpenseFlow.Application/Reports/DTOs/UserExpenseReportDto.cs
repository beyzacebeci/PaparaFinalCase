namespace ExpenseFlow.Application.Reports.DTOs;

    public class UserExpenseReportDto : ExpenseReportDto
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
    }

