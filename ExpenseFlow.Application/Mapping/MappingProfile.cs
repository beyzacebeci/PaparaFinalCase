using AutoMapper;
using ExpenseFlow.Application.ExpenseCategories;
using ExpenseFlow.Application.ExpenseClaims;
using ExpenseFlow.Application.PaymentTransactions;
using ExpenseFlow.Application.Users;
using ExpenseFlow.DataAccess.ExpenseCategories;
using ExpenseFlow.DataAccess.ExpenseClaims;
using ExpenseFlow.DataAccess.PaymentTransactions;
using ExpenseFlow.DataAccess.Users;

namespace ExpenseFlow.Application.Mapping;

public class MappingProfile : Profile
    
{
    public MappingProfile()
    {
        CreateMap<ExpenseClaim,ExpenseClaimResponse>().ReverseMap();
        CreateMap<ExpenseClaimRequest, ExpenseClaim>();

        CreateMap<PaymentTransaction, PaymentTransactionResponse>().ReverseMap();
        CreateMap<PaymentTransactionRequest, PaymentTransaction>();

        CreateMap<ExpenseCategory, ExpenseCategoryResponse>().ReverseMap();
        CreateMap<ExpenseCategoryRequest, ExpenseCategory>();

        CreateMap<UserRegistrationRequest, User>();
        

    }
}

