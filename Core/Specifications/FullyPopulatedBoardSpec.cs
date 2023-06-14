using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Specifications;

public class FullyPopulatedBoardSpec : BaseSpecification<Board>
{
    public FullyPopulatedBoardSpec() : base()
    {
        AddOrderBy(a => a.Order);
        AddInclude(a => a
            .Include(b => b.Todos.OrderBy(d => d.Order))
            .ThenInclude(c => c.SubTasks.OrderBy(e => e.Order)));
    }

    public FullyPopulatedBoardSpec(int boardId) : base(b => b.Id == boardId)
    {
        AddOrderBy(a => a.Order);
        AddInclude(a => a
            .Include(b => b.Todos.OrderBy(d => d.Order))
            .ThenInclude(c => c.SubTasks.OrderBy(e => e.Order)));
    }
}
