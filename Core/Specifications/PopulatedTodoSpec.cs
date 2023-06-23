using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.Specifications;

public class PopulatedTodoSpec : BaseSpecification<Todo>
{
    public PopulatedTodoSpec(int todoId, string userId)
        : base(t => t.Id == todoId && t.AppUserId == userId)
    {
        AddInclude(a => a.Include(b => b.SubTasks.OrderBy(c => c.Order)));
    }
}
