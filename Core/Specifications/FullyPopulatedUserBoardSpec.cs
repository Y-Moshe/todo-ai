using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Specifications;

public class FullyPopulatedUserBoardSpec : BaseSpecification<Board>
{
    public FullyPopulatedUserBoardSpec(string userId)
        : base(b => b.AppUserId == userId)
    {
        AddOrderBy(a => a.Order);
        AddInclude(a => a
            .Include(b => b.Todos.OrderBy(d => d.Order))
            .ThenInclude(c => c.SubTasks.OrderBy(e => e.Order)));
    }

    public FullyPopulatedUserBoardSpec(int boardId, string userId)
        : base(b => b.Id == boardId && b.AppUserId == userId)
    {
        AddOrderBy(a => a.Order);
        AddInclude(a => a
            .Include(b => b.Todos.OrderBy(d => d.Order))
            .ThenInclude(c => c.SubTasks.OrderBy(e => e.Order)));
    }
}
