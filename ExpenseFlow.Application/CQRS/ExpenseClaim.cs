using ExpenseFlow.Application.ExpenseClaims;
using MediatR;

namespace ExpenseFlow.Application.CQRS;

public record GetAllExpenseClaimsQuery : IRequest<ApiResponse<List<ExpenseClaimResponse>>>;
public record GetExpenseClaimByIdQuery(int Id) : IRequest<ApiResponse<ExpenseClaimResponse>>;
public record CreateExpenseClaimCommand(ExpenseClaimRequest ExpenseClaim) : IRequest<ApiResponse<ExpenseClaimResponse>>;
public record UpdateExpenseClaimCommand(int Id, ExpenseClaimRequest ExpenseClaim) : IRequest<ApiResponse>;
public record DeleteAccountCommand(int Id) : IRequest<ApiResponse>;