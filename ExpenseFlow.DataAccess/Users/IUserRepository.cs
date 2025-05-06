using System.Collections.Generic;
using System.Threading.Tasks;
using ExpenseFlow.DataAccess.Users;

namespace ExpenseFlow.DataAccess.Users;

public interface IUserRepository
{
    Task<List<User>> GetAllWithClaimsAsync();
    Task<User> GetByIdAsync(string id);
    Task<User> GetByIdWithClaimsAsync(string id);
}