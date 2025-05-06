using System;

namespace ExpenseFlow.Application.Reports.DTOs;

public class ExpenseReportDto
{
    public decimal TotalAmount { get; set; }
    public int TotalClaims { get; set; }
    public int ApprovedClaims { get; set; }
    public int RejectedClaims { get; set; }
    public int PendingClaims { get; set; }
    public int PaidClaims { get; set; }
    public decimal ApprovedAmount { get; set; }
    public decimal RejectedAmount { get; set; }
    public decimal PendingAmount { get; set; }
    public decimal PaidAmount { get; set; }
}

