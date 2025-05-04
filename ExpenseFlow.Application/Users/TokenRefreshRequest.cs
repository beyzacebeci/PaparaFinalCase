using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseFlow.Application.Users;

public class TokenRefreshRequest
{
    public String AccessToken { get; init; }
    public String RefreshToken { get; init; }

}

