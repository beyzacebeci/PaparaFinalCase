using AutoMapper;
using ExpenseFlow.Application.ExpenseClaims;
using ExpenseFlow.DataAccess.ExpenseClaims;

namespace ExpenseFlow.Application.Mapping;

public class MappingProfile : Profile
    
{
    public MappingProfile()
    {
        CreateMap<ExpenseClaim,ExpenseClaimResponse>().ReverseMap(); 
    }
}

