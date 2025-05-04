using AutoMapper;
using ExpenseFlow.Application.ExpenseClaims;
using ExpenseFlow.Application.Users;
using ExpenseFlow.DataAccess.ExpenseClaims;
using ExpenseFlow.DataAccess.Users;

namespace ExpenseFlow.Application.Mapping;

public class MappingProfile : Profile
    
{
    public MappingProfile()
    {
        CreateMap<ExpenseClaim,ExpenseClaimResponse>().ReverseMap();

        CreateMap<UserRegistrationRequest, User>();

    }
}

