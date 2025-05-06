using ExpenseFlow.DataAccess.ExpenseClaims;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseFlow.DataAccess.ExpenseCategories
{
    public class ExpenseCategoryRepository : GenericRepository<ExpenseCategory, int>, IExpenseCategoryRepository
    {
        private readonly ExpenseFlowDbContext _context;

        public ExpenseCategoryRepository(ExpenseFlowDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<ExpenseCategory>> GetAllIncludingClaimsAsync()
        {
            return await _context.ExpenseCategory
                .Include(c => c.ExpenseClaims)
                .Where(c => c.ExpenseClaims.Any())
                .ToListAsync();
        }
    }
}
