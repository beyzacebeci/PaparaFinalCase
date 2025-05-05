using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseFlow.Application.PaymentTransactions
{
    public record PaymentTransactionResponse
    {
        public int Id { get; init; }
        public int ExpenseClaimId { get; init; }
        public string UserId { get; init; }
        public decimal Amount { get; init; }
        public DateTime PaymentDate { get; init; }
        public string PaymentReference { get; init; } = default!;
        public string PaymentStatus { get; init; }
    }
}
