using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Specifications;

public class PopulatedBoardTodosSpec : BaseSpecification<Todo>
{
    public PopulatedBoardTodosSpec(int boardId)
        : base(t => t.BoardId == boardId)
    {
        AddOrderBy(a => a.Order);
        AddInclude(a => a.Include(b => b.SubTasks));
    }
}
