using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Specifications;

public class FullyPopulatedBoardSpec : BaseSpecification<Board>
{
    public FullyPopulatedBoardSpec(int boardId) : base(b => b.Id == boardId)
    {
        AddInclude(a => a
            .Include(b => b.Todos)
            .ThenInclude(c => c.SubTasks));
    }
}
